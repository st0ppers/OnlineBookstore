using System.Net;
using AutoMapper;
using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler.AuthorCommandHandler
{
    public class AuthorDeleteCommandHandler : IRequestHandler<AuthorDeleteCommand, AddAuthorResponse>
    {
        private readonly IAuthorRepo _authorRepo;

        public AuthorDeleteCommandHandler(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<AddAuthorResponse> Handle(AuthorDeleteCommand request, CancellationToken cancellationToken)
        {

            if (await _authorRepo.GetById(request.id) == null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }
            var result = await _authorRepo.DeleteAuthor(request.id);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }
    }
}
