namespace BookStore.Models.Models
{
    public record  AuthorAdditionalInfo
    {
        public int AuthorId { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
