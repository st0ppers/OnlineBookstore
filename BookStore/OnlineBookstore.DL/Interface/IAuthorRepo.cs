using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IAuthorRepo
    {
        private static List<Author> _authors;
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<Author?> GetById(int id);
        public Task<Author?> GetAuthorByName(string name);
        public Task<Author?> AddAuthor(Author? author);
        public Task<Author?> UpdateAuthor(Author? author);
        public Task<Author> DeleteAuthor(int authorId);
        public Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
