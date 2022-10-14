namespace BookStore.Cache
{
    public class KafkaCache<TKey, TValue>
    {
        public  Dictionary<TKey, TValue> cache;

        public KafkaCache()
        {
            cache = new Dictionary<TKey, TValue>();
        }
    }
}
