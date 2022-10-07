using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands.AuthorCommands
{
    public record AuthorDeleteCommand(int id) :IRequest<AddAuthorResponse>
    {
    }
}
