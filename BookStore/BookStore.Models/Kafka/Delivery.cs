using BookStore.Cache;
using BookStore.Models.Models;
using MessagePack;

namespace BookStore.Models.Kafka
{
    [MessagePackObject]
    public class Delivery : ICacheModel<int>
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public Book Book { get; set; }
        [Key(2)]
        public DateTime LastUpdated { get; set; }
        [Key(3)]
        public int Quantity { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
