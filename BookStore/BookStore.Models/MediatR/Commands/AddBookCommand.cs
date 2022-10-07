using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    //                              parametur
    public record AddBookCommand(AddBookRequest book) : IRequest<AddBookResponse>
    {
        
    }
}
