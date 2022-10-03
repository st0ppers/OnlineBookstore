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

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepo.GetAllBooks();
        }

        public Book GetById(int id)
        {
            return _bookRepo.GetById(id);
        }

        public Book GetByTitle(string title)
        {
            return _bookRepo.GetByTitle(title);
        }
        public AddBookResponse AddBook(AddBookRequest? bookRequest)
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
            var result = _bookRepo.AddBook(book);
            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }

        public AddBookResponse UpdateBook(AddBookRequest bookRequest)
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
            var result = _bookRepo.UpdateBook(book);
            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }

        public Book DeleteBook(int bookId)
        {
            return _bookRepo.DeleteBook(bookId);
        }
    }
}
