using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record GetBookByTitleCommand(string title) :IRequest<Book>
    {
    }
}
