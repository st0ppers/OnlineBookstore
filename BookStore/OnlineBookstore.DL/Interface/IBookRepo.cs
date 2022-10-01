using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IBookRepo
    {
        public static List<Book> _books { get; set; }
        public IEnumerable<Book> GetAllBooks();
        public Book GetById(int id);
        public Book AddBook(Book book);
        public Book UpdateBook(Book book);
        public Book DeleteBook(int bookId);
    }
}
