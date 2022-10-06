using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler
{
    public class GetAllBooksCommandHandler :IRequestHandler<GetAllBooksCommand,IEnumerable<Book>>
    {
        private readonly IBookRepo _bookRepo;

        public GetAllBooksCommandHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.GetAllBooks();
        }
    }
}
