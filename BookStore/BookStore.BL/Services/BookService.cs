using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        public BookService(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepo.GetAllBooks();
        }

        public Book GetById(int id)
        {
            return _bookRepo.GetById(id);
        }

        public Book AddBook(Book book)
        {
            return _bookRepo.AddBook(book);
        }

        public Book UpdateBook(Book book)
        {
            return _bookRepo.UpdateBook(book);
        }

        public Book DeleteBook(int bookId)
        {
            return _bookRepo.DeleteBook(bookId);
        }
    }
}
