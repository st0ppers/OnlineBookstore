using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet(nameof(GetAll))]
        public IEnumerable<Book> GetAll()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet(nameof(GetById))]
        public Book GetById(int id)
        {
            return _bookService.GetById(id);
        }

        [HttpPost(nameof(AddBook))]
        public Book AddBook(Book book)
        {
            return _bookService.AddBook(book);
        }

        [HttpPut(nameof(UpdateBook))]
        public Book UpdateBook(Book book)
        {
            return _bookService.UpdateBook(book);
        }

        [HttpDelete(nameof(DeleteBook))]
        public Book DeleteBook(int bookId)
        {
            return _bookService.DeleteBook(bookId);
        }
    }
}
