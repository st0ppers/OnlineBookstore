using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public IEnumerable<Author> GetAllAuthors();
        public Guid GetGuidId();
        public Author GetById(int id);
        public Author AddAuthor(Author author);
        public Author UpdateAuthor(Author author);
        public Author DeleteAuthor(int authorId);
    }
}
