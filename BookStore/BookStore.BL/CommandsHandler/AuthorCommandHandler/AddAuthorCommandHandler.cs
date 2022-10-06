using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler.AuthorCommandHandler
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorResponse>
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;

        public AddAuthorCommandHandler(IAuthorRepo authorSrRepo, IMapper mapper)
        {
            _authorRepo = authorSrRepo;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (await _authorRepo.GetAuthorByName(request.Request.Name) != null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }

            var author = _mapper.Map<Author>(request.Request);
            var result = await _authorRepo.AddAuthor(author);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }
    }
}
