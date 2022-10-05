using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetById(int id);
        public Task<Book> GetByTitle(string title);
        public Task<AddBookResponse?> AddBook(AddBookRequest bookRequest);
        public Task<AddBookResponse> UpdateBook(AddBookRequest bookRequest);
        public Task<Book> DeleteBook(int bookId);
    }
}
