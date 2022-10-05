using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IBookRepo
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetById(int id);
        public Task<Book> GetByTitle(string tilte);
        public Task<bool> GetByAuthorId(int id);
        public Task<Book?> AddBook(Book book);
        public Task<Book> UpdateBook(Book book);
        public Task<Book> DeleteBook(int bookId);
    }
}
