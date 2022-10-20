namespace BookStore.Cache
{
    public interface ICacheModel<out T>
    {
        public DateTime LastUpdated { get; set; }
        public int Quantity { get; set; }
        public T GetKey();
    }
}
