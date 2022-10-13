using BookStore.BL.Serializers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class ProducerServices<TKey,TValue>
    {
        private ProducerConfig _config;
        private readonly IOptionsMonitor<KafkaSettings.KafkaSettings> _kafkaSettings;
        public ProducerServices(IOptionsMonitor<KafkaSettings.KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers,
            };
        }

        public async Task Producer(TValue person,TKey k)
        {
            var producer = new ProducerBuilder<TKey, TValue>(_config).SetKeySerializer(new MsgPackSserializer<TKey>())
                .SetValueSerializer(new MsgPackSserializer<TValue>())
                .Build();

            try
            {
                var mesasge = new Message<TKey, TValue>()
                {
                    Key = k,
                    Value = person,
                };
                var result = await producer.ProduceAsync("test2", mesasge);

                if (result != null)
                {
                    Console.WriteLine($"------------------ Delivered key {result.Key} value: {result.Value}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
