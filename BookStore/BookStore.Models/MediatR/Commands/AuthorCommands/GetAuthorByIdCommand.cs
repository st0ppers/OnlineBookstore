using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands.AuthorCommands
{
    public record GetAuthorByIdCommand(int id) : IRequest<Author>
    {
    }
}
