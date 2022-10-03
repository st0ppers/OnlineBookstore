using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public  interface IBookService
    {
        public IEnumerable<Book> GetAllBooks();
        public Book GetById(int id);
        public Book  GetByTitle(string title);
        public AddBookResponse? AddBook(AddBookRequest bookRequest);
        public AddBookResponse UpdateBook(AddBookRequest bookRequest);
        public Book DeleteBook(int bookId);
    }
}
