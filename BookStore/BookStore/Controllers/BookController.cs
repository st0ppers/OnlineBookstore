using System.Net;
using BookStore.BL.Interfaces;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookService.GetAllBooks());
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Parameter id {id} must be greater than 0");
            }
            var result = await _bookService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost(nameof(AddBook))]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest bookRequest)
        {
            var result = await _bookService.AddBook(bookRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPut(nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook([FromBody] AddBookRequest bookRequest)
        {
            var result = await _bookService.UpdateBook(bookRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            return Ok(await _bookService.DeleteBook(bookId));
        }
    }
}
