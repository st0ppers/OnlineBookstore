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
            return Ok(await _authorServices.GetById(id));
        }

        [HttpGet(nameof(GetByNameAuthor))]
        public IActionResult? GetByNameAuthor(string name)
        {
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
                return BadRequest(res);
            }

            return Ok(res);
        }
        [HttpPut(nameof(Update))]
        public async Task<IActionResult?> Update([FromBody] AddAuthorRequest? authorRequest)
        {
            var existing = await _authorServices.GetById(authorRequest.Id);

            if (existing.Name == null)
            {
                return NotFound(existing);
            }
            await _authorServices.UpdateAuthor(authorRequest);
            return Ok(existing);
        }
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult?> Delete([FromBody] int authorId)
        {
            return Ok(await _authorServices.DeleteAuthor(authorId));
        }
    }
}
