using BookStore.BL.Interfaces;
using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler.AuthorCommandHandler
{
    public class GetAuthorByIdCommandHandler : IRequestHandler<GetAuthorByIdCommand, Author>
    {
        private readonly IAuthorRepo _authroRepo;

        public GetAuthorByIdCommandHandler(IAuthorRepo authorService)
        {
            _authroRepo = authorService;
        }

        public async Task<Author> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            return await _authroRepo.GetById(request.id);
        }
    }
}
