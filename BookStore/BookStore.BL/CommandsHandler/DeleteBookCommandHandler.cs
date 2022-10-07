using BookStore.BL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.CommandsHandler
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IBookRepo _bookRepo;

        public DeleteBookCommandHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.DeleteBook(request.bookId);
        }
    }
}
