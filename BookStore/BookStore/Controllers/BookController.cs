using System.Net;
using BookStore.BL.Kafka;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private  ConsumerService<int, Book> _kafkaConsumer;

        public BookController(IMediator mediator, ConsumerService<int, Book> kafkaConsumer)
        {
            _mediator = mediator;
            _kafkaConsumer = kafkaConsumer;
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var a = _kafkaConsumer._kafkaCache.cache;
            return Ok(a);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
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
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        [HttpDelete(nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var result = await _mediator.Send(new DeleteBookCommand(bookId));
            if (bookId <= 0)
            {
                return BadRequest($"Parameter id {bookId} must be greater than 0");
            }
            return Ok(result.Id);
        }
    }
}
