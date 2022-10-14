using BookStore.BL.Serializers;
using BookStore.Cache;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class ConsumerService<TKey, TValue> : IHostedService where TValue : ICacheModel<TKey>
    {
        private readonly IConsumer<TKey, TValue> _consumer;
        public KafkaCache<TKey, TValue> _kafkaCache;
        public ConsumerService(IOptionsMonitor<KafkaSettings.KafkaSettings> kafkaSettings, KafkaCache<TKey, TValue> result)
        {
            var kafkaSettings1 = kafkaSettings;
            _kafkaCache = result;
            var config = new ConsumerConfig()
            {
                BootstrapServers = kafkaSettings1.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = kafkaSettings1.CurrentValue.GroupId,
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config).SetKeyDeserializer(new MsgPackDeserializer<TKey>())
                .SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();
            _consumer.Subscribe(kafkaSettings1.CurrentValue.Topic);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var item = _consumer.Consume();
                    _kafkaCache.cache.Add(item.Message.Key, item.Message.Value);
                    Console.WriteLine($"consumed key:{item.Message.Key}");
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
