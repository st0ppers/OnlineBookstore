using BookStore.BL.Kafka.KafkaSettings;
using BookStore.BL.Serializers;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class ConsumerService<TKey, TValue> : IHostedService, IDisposable
    {
        private readonly IOptionsMonitor<KafkaSettings.KafkaSettings> _kafkaSettings;
        private ConsumerConfig _config;
        private IConsumer<TKey, TValue> consumer;
        public ConsumerService(IOptionsMonitor<KafkaSettings.KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "MyGroupID",
            };
            consumer = new ConsumerBuilder<TKey, TValue>(_config).SetKeyDeserializer(new MsgPackDeserializer<TKey>())
                .SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();

            consumer.Subscribe("test2");
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("It is starting -------------------------");
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume();
                    Console.WriteLine($"Recieved Key: {result.Message.Key} Value: {result.Message.Value}");
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            consumer.Dispose();
        }
    }
}
