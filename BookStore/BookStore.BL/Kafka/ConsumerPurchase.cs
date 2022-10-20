using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Threading.Tasks.Dataflow;
using BookStore.BL.HttpClient;
using BookStore.BL.Kafka.Settings;
using BookStore.BL.Serializers;
using BookStore.Models.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.Extensions.Options;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Kafka
{
    public class ConsumerPurchase : IMyConsumer
    {
        private readonly IConsumer<Guid, Purchase> _consumer;
        private readonly IOptionsMonitor<KafkaSettingsPurchase> _settings;
        private readonly TransformBlock<Purchase, string> _transformBlock;
        private readonly IOptionsMonitor<HttpClientSettings> _httpClientSettings;

        public ConsumerPurchase(IBookRepo bookRepo, IOptionsMonitor<KafkaSettingsPurchase> settings, IOptionsMonitor<HttpClientSettings> httpClientSettings)
        {
            var bookRepo1 = bookRepo;
            _settings = settings;
            _httpClientSettings = httpClientSettings;
            var config = new ConsumerConfig()
            {
                BootstrapServers = _settings.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _settings.CurrentValue.GroupId
            };

            _consumer = new ConsumerBuilder<Guid, Purchase>(config).SetKeyDeserializer(new MsgPackDeserializer<Guid>())
                .SetValueDeserializer(new MsgPackDeserializer<Purchase>()).Build();
            _consumer.Subscribe(_settings.CurrentValue.Topic);
            var client = new Client(_httpClientSettings);

            _transformBlock = new TransformBlock<Purchase, string>(async purchase =>
            {
                var additinalAuthorInfo = await client.GetAdditionalInfo();

                var books = purchase.Books;
                foreach (var item in books)
                {
                    var book = await bookRepo1.GetById(item.Id);
                    if (book == null)
                    {
                        book.Title = $"Book {book.Id} aaa";
                        await bookRepo.AddBook(book);
                    }

                    var s = additinalAuthorInfo.Distinct().FirstOrDefault(x => book.AuthorId == x.Key);
                    purchase.AdditionalInfo.Append(s.Value);
                    
                    book.Quantity -= item.Quantity;
                    await bookRepo1.UpdateBook(item);

                    return $"Book id {item.Id}";
                }
                return "";

            });

            var actionBlock = new ActionBlock<string>(Console.WriteLine);

            _transformBlock.LinkTo(actionBlock);
        }
        public Task MyConsume()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var a = _consumer.Consume();
                        _transformBlock.Post(a.Message.Value);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            });
            return Task.CompletedTask;
        }
    }
}
