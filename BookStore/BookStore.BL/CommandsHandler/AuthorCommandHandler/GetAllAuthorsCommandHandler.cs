using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler.AuthorCommandHandler
{
    public class GetAllAuthorsCommandHandler :IRequestHandler<GetAllAuthorsCommand,IEnumerable<Author>>
    {
        private readonly IAuthorRepo _authorRepo;

        public GetAllAuthorsCommandHandler(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.GetAllAuthors();
        }
    }
}
