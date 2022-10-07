using System.Net;
using BookStore.Midlewear;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            throw new CustomException($"This error is from {nameof(GetAll)} ");
            return Ok(await _mediator.Send(new GetAllBooksCommand()));
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Parameter id {id} must be greater than 0");
            }

            var result = await _mediator.Send(new GetByIdBookCommand(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost(nameof(AddBook))]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest bookRequest)
        {
            var result = await _mediator.Send(new AddBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(result);
            }

            return Ok(result.Book);
        }

        [HttpPut(nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook([FromBody] AddBookRequest bookRequest)
        {
            var result = await _mediator.Send(new UpdateBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var result = await _mediator.Send(new DeleteBookCommand(bookId));
            if (bookId <= 0)
            {
                return BadRequest($"Parameter id {bookId} must be greater than 0");
            }
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
