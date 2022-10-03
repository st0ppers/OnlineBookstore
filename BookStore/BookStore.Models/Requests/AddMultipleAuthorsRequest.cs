using BookStore.Models.Models;

namespace BookStore.Models.Requests
{
    public class AddMultipleAuthorsRequest
    {
        public IEnumerable<AddAuthorRequest> Authors { get; set; }
        public string Reason { get; set; }
    }
}
