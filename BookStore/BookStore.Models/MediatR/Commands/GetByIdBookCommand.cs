using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record GetByIdBookCommand(int id) : IRequest<Book>
    {
    }
}
