using System.Threading.Tasks.Dataflow;
using BookStore.BL.Kafka.Settings;
using BookStore.BL.Serializers;
using BookStore.Models.Kafka;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Kafka
{
    public class ConsumerDelivery : IMyConsumer
    {
        private readonly IConsumer<int, Delivery> _consumer;
        private readonly TransformBlock<Delivery, string> _transformBlock;

        public ConsumerDelivery(IOptionsMonitor<KafkaSettingsDelivery> settings, IBookRepo bookRepo)
        {
            var settings1 = settings;
            var bookRepo1 = bookRepo;
            var config = new ConsumerConfig()
            {
                BootstrapServers = settings1.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = settings1.CurrentValue.GroupId
            };

            _consumer = new ConsumerBuilder<int, Delivery>(config).SetKeyDeserializer(new MsgPackDeserializer<int>())
                .SetValueDeserializer(new MsgPackDeserializer<Delivery>()).Build();
            _consumer.Subscribe(settings1.CurrentValue.Topic);

            _transformBlock = new TransformBlock<Delivery, string>(async x =>
            {
                var book = await bookRepo1.GetById(x.Book.Id);

                if (book == null)
                {
                    book.Title = $"Book {book.Id} aaa";
                   await bookRepo.AddBook(x.Book);
                }
                book.Quantity += x.Quantity;
                await bookRepo1.UpdateBook(book);

                return $"Book id {book.Id} with quantity {book.Quantity}";
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
                    var a = _consumer.Consume().Message.Value;
                    _transformBlock.Post(a);
                }
            });
            return Task.CompletedTask;
        }
    }
}
