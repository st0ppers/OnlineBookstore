namespace BookStore.BL.Kafka.Settings
{
    public class KafkaSettingsPurchase
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
        public string Topic { get; set; }
    }
}
