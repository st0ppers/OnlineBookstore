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
        public IActionResult GetAll()
        {
            _logger.LogInformation("Information Test");
            _logger.LogWarning("Warning Test");
            _logger.LogError("Error Test");
            _logger.LogCritical("Critical Test");
            return Ok(_bookService.GetAllBooks());
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Parameter id {id} must be greater than 0");
            }
            var result = _bookService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost(nameof(AddBook))]
        public IActionResult AddBook([FromBody] AddBookRequest bookRequest)
        {
            var result = _bookService.AddBook(bookRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut(nameof(UpdateBook))]
        public IActionResult UpdateBook([FromBody] AddBookRequest bookRequest)
        {
            var result = _bookService.GetByTitle(bookRequest.Title);

            if (result.Title == null)
            {
                return NotFound(result);
            }

            _bookService.UpdateBook(bookRequest);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteBook))]
        public IActionResult DeleteBook(int bookId)
        {
            return Ok(_bookService.DeleteBook(bookId));
        }
    }
}
