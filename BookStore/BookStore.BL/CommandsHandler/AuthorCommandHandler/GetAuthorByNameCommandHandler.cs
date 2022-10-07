using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler.AuthorCommandHandler
{
    public class GetAuthorByNameCommandHandler :IRequestHandler<GetAuhtorByNameCommand,Author>
    {
        private readonly IAuthorRepo _authorRepo;

        public GetAuthorByNameCommandHandler(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(GetAuhtorByNameCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.GetAuthorByName(request.name);
        }
    }
}
