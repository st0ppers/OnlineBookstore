using System.Net;
using AutoMapper;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler
{
    //                                                  kommanda ,     kakvo vrushtash
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        public AddBookCommandHandler(IBookRepo bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand book, CancellationToken cancellationToken)
        {
           
                if (await _bookRepo.GetByTitle(book.book.Title) != null)
                {
                    return new AddBookResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Bad Request"
                    };
                }
                var bookMapper = _mapper.Map<Book>(book);
                var result = await _bookRepo.AddBook(bookMapper);
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = result
                };
            
            
        }
    }
}
