namespace BookStore.BL.Kafka.Settings
{
    public class KafkaSettingsDelivery
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
        public string Topic { get; set; }
    }
}
