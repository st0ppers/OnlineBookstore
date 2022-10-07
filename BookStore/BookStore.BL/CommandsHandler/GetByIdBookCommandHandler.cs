using BookStore.BL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler
{
    public class GetByIdBookCommandHandler : IRequestHandler<GetByIdBookCommand, Book>
    {
        private readonly IBookRepo _bookRepo;

        public GetByIdBookCommandHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(GetByIdBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.GetById(request.id);
        }
    }
}
