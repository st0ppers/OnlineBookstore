using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Models.Models
{
    public record Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; init; }
        public List<Book> Books { get; set; }
        public decimal TotalMoney { get; set; }
        public int UserId { get; set; }
    }
}
