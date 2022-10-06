using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private static IAuthorService _authorServices;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService authorServices, IMapper mapper)
        {
            _authorServices = authorServices;
            _mapper = mapper;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _authorServices.GetAllAuthors());
        }
        [HttpGet(nameof(GetByID))]
        public async Task<IActionResult?> GetByID(int id)
        {
            var user = await _authorServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(await _authorServices.GetById(id));
        }

        [HttpGet(nameof(GetByNameAuthor))]
        public async Task<IActionResult?> GetByNameAuthor(string name)
        {
            var auth = await _authorServices.GetAuthorByName(name);
            if (auth == null)
            {
                return BadRequest(auth);
            }
            return Ok(_authorServices.GetAuthorByName(name));
        }

        [HttpPost(nameof(AddAuthorRange))]
        public async Task<IActionResult> AddAuthorRange([FromBody] AddMultipleAuthorsRequest addMultipleAuthorsRequest)
        {
            if (addMultipleAuthorsRequest != null && !addMultipleAuthorsRequest.Authors.Any())
            {
                return BadRequest(addMultipleAuthorsRequest);
            }

            var authorCollection = _mapper.Map<IEnumerable<Author>>(addMultipleAuthorsRequest.Authors);

            var result = await _authorServices.AddMultipleAuthors(authorCollection);

            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(Add))]
        public async Task<IActionResult> Add([FromBody] AddAuthorRequest authorRequest)
        {
            var res = await _authorServices.AddAuthor(authorRequest);
            if (res.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPut(nameof(Update))]
        public async Task<IActionResult?> Update([FromBody] AddAuthorRequest? authorRequest)
        {
            var auth = await _authorServices.UpdateAuthor(authorRequest);
            if (auth.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return NotFound(auth);
            }
            return Ok(auth);
        }
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult?> Delete([FromBody] int id)
        {
            var auth = await _authorServices.GetById(id);
            if (auth == null)
            {
                return NotFound(auth);
            }

            await _authorServices.DeleteAuthor(id);
            return Ok(auth);
        }
    }
}
