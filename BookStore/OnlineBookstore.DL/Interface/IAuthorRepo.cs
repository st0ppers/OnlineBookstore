using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IAuthorRepo
    {
        private static List<Author> _authors;
        public IEnumerable<Author> GetAllAuthors();
        public Author GetById(int id);
        public Author AddAuthor(Author author);
        public Author UpdateAuthor(Author author);
        public Author DeleteAuthor(int authorId);
    }
}
