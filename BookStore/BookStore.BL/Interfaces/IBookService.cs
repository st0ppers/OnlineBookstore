using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public  interface IBookService
    {
        public IEnumerable<Book> GetAllBooks();
        public Book GetById(int id);
        public Book AddBook(Book book);
        public Book UpdateBook(Book book);
        public Book DeleteBook(int bookId);
    }
}
