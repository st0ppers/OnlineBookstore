using BookStore.Models.Models;
using MessagePack;

namespace BookStore.Models.Kafka
{
    [MessagePackObject()]
    public class PurchaseKafka
    {
        [Key(0)]
        public Guid Id { get; init; }
        [Key(1)]
        public List<Book> Books { get; set; }
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int UserId { get; set; }
    }
}
