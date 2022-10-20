using MessagePack;
using BookStore.Cache;
namespace BookStore.Models.Models
{
    [MessagePackObject]
    public class Book : ICacheModel<int>
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int AuthorId { get; init; }
        [Key(2)]
        public string Title { get; set; }
        [Key(3)]
        public DateTime LastUpdated { get; set; }
        [Key(4)]
        public int Quantity { get; set; }
        [Key(5)]
        public decimal Price { get; set; }

        public int GetKey()
        {
            return Id;
        }

    }
}
