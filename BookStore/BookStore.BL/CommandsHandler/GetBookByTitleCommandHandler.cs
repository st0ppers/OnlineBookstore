using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler
{
    public class GetBookByTitleCommandHandler : IRequestHandler<GetBookByTitleCommand,Book>
    {
        private readonly IBookRepo _bookRepo;

        public GetBookByTitleCommandHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(GetBookByTitleCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.GetByTitle(request.title);
        }
    }
}
