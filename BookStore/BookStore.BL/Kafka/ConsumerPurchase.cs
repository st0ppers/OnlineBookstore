using System.Threading.Channels;
using System.Threading.Tasks.Dataflow;
using BookStore.BL.Kafka.Settings;
using BookStore.BL.Serializers;
using BookStore.Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Kafka
{
    public class ConsumerPurchase : IMyConsumer
    {
        private readonly IConsumer<Guid, Purchase> _consumer;
        private readonly IOptionsMonitor<KafkaSettingsPurchase> _settings;
        private readonly TransformBlock<Purchase, string> _transformBlock;

        public ConsumerPurchase(IBookRepo bookRepo, IOptionsMonitor<KafkaSettingsPurchase> settings)
        {
            var bookRepo1 = bookRepo;
            _settings = settings;
            var config = new ConsumerConfig()
            {
                BootstrapServers = _settings.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _settings.CurrentValue.GroupId
            };

            _consumer = new ConsumerBuilder<Guid, Purchase>(config).SetKeyDeserializer(new MsgPackDeserializer<Guid>())
                .SetValueDeserializer(new MsgPackDeserializer<Purchase>()).Build();
            _consumer.Subscribe(_settings.CurrentValue.Topic);



            _transformBlock = new TransformBlock<Purchase, string>(async x =>
            {
                var books = x.Books;

                foreach (var book in books)
                {
                    var i = await bookRepo1.GetById(book.Id);
                    if (i == null)
                    {
                        i.Title = $"Book {i.Id} aaa";
                        await bookRepo.AddBook(i);
                    }
                    i.Quantity -= book.Quantity;
                    await bookRepo1.UpdateBook(book);

                    return $"Book id {book.Id}";
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
