using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Models.Models
{
    [MessagePackObject()]
    public record Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [Key(0)]
        public Guid Id { get; init; }
        [Key(1)]
        public List<Book> Books { get; set; }
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int UserId { get; set; }
        [Key(4)] public IEnumerable<string> AdditionalInfo { get; set; } = Enumerable.Empty<string>();

    }
}
