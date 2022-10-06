using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands.AuthorCommands
{
    public record GetAuhtorByNameCommand(string name) : IRequest<Author>
    {
    }
}
