using System.Net;
using BookStore.Models.MediatR.Commands.AuthorCommands;
using BookStore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
        }
        [HttpGet(nameof(GetByID))]
        public async Task<IActionResult?> GetByID(int id)
        {
            var user = await _mediator.Send(new GetAuthorByIdCommand(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet(nameof(GetByNameAuthor))]
        public async Task<IActionResult?> GetByNameAuthor(string name)
        {
            var auth = await _mediator.Send(new GetAuhtorByNameCommand(name));
            if (auth == null)
            {
                return BadRequest(auth);
            }
            return Ok(auth);
        }
        [HttpPost(nameof(Add))]
        public async Task<IActionResult> Add([FromBody] AddAuthorRequest authorRequest)
        {
            var res = await _mediator.Send(new AddAuthorCommand(authorRequest));
            if (res.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPut(nameof(Update))]
        public async Task<IActionResult?> Update([FromBody] UpdateAuthorRequest? authorRequest)
        {
            var auth = await _mediator.Send(new AuthorUpdateCommand(authorRequest));
            if (auth.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(auth);
            }
            return Ok(auth);
        }
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult?> Delete([FromBody] int id)
        {
            var auth = await _mediator.Send(new AuthorDeleteCommand(id));
            if (auth == null)
            {
                return NotFound(auth);
            }
            return Ok(auth);
        }
    }
}
