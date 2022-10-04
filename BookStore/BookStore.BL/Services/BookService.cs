using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        public BookService(IBookRepo bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepo.GetAllBooks();
        }

        public async Task<Book> GetById(int id)
        {
            return await _bookRepo.GetById(id);
        }

        public async Task<Book> GetByTitle(string title)
        {
            return await _bookRepo.GetByTitle(title);
        }
        public async Task<AddBookResponse> AddBook(AddBookRequest? bookRequest)
        {
            if (_bookRepo.GetByTitle(bookRequest.Title) == null)
            {
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad Request"
                };
            }
            var book = _mapper.Map<Book>(bookRequest);
            var result = await _bookRepo.AddBook(book);
            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }

        public async Task<AddBookResponse> UpdateBook(AddBookRequest bookRequest)
        {
            if (_bookRepo.GetByTitle(bookRequest.Title) != null)
            {
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad Request"
                };
            }
            var book = _mapper.Map<Book>(bookRequest);
            var result = await _bookRepo.UpdateBook(book);
            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }

        public async Task<Book> DeleteBook(int bookId)
        {
            return await _bookRepo.DeleteBook(bookId);
        }
    }
}
