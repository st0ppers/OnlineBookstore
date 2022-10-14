namespace BookStore.Cache
{
    public interface ICacheModel<out T>
    {
        public DateTime LastUpdated { get; set; }
        public T GetKey();
    }
}
