using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands.AuthorCommands
{
    public record AuthorUpdateCommand(UpdateAuthorRequest Request) : IRequest<AddAuthorResponse>
    {
    }
}
