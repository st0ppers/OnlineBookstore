using System.Net;
using AutoMapper;
using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler.AuthorCommandHandler
{
    public class AuthoreUpdateCommandHandler : IRequestHandler<AuthorUpdateCommand,AddAuthorResponse>
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;
        public AuthoreUpdateCommandHandler(IAuthorRepo authorRepo, IMapper mapper)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> Handle(AuthorUpdateCommand request, CancellationToken cancellationToken)
        {
            if (await _authorRepo.GetAuthorByName(request.Request.Name) == null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }
            var author = _mapper.Map<Author>(request.Request);
            var result = await _authorRepo.UpdateAuthor(author);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }
    }
}
