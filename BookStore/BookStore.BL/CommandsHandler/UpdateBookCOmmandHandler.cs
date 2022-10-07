using System.Net;
using AutoMapper;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler
{
    internal class UpdateBookCOmmandHandler : IRequestHandler<UpdateBookCommand,AddBookResponse>
    {
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;

        public UpdateBookCOmmandHandler(IBookRepo bookService, IMapper mapper)
        {
            _bookRepo = bookService;
            _mapper = mapper;
        }

        public async Task<AddBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {

            if (await _bookRepo.GetByTitle(request.bookRequest.Title) == null)
            {
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad Request"
                };
            }
            var book = _mapper.Map<Book>(request.bookRequest);
            var result = await _bookRepo.UpdateBook(book);
            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }
    }
}
