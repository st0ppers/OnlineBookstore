using System.Net;
using System.Net.NetworkInformation;
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

        public AuthorController(IAuthorService authorServices)
        {
            _authorServices = authorServices;

        }

        [HttpGet(nameof(Get))]
        public IActionResult Get()
        {
            return Ok(_authorServices.GetAllAuthors());
        }

        [HttpGet(nameof(GetByID))]
        public IActionResult? GetByID(int id)
        {
            if (id <= 0) return BadRequest($"Parameter id {id} must be greater than 0");
            var result = _authorServices.GetById(id);

            if (result == null)
            {
                return NotFound(id);
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetByNameAuthor))]
        public IActionResult? GetByNameAuthor(string name)
        {
            return Ok(_authorServices.GetAuthorByName(name));
        }

        [HttpPost(nameof(Add))]
        public IActionResult Add([FromBody] AddAuthorRequest authorRequest)
        {
            //if (authorRequest == null)
            //{
            //    return BadRequest(authorRequest);
            //}

            //var authorExist = _authorServices.GetAuthorByName(authorRequest.Name);
            //if (authorExist != null) return BadRequest("Author exists!");

            //return Ok(_authorServices.AddAuthor(new Author()));



            //change AddAuthor in services / repo param to AddAuthorRequest

            var res = _authorServices.AddAuthor(authorRequest);

            if (res.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
        [HttpPut(nameof(Update))]
        public IActionResult? Update([FromBody] AddAuthorRequest? authorRequest)
        {
            var existing = _authorServices.GetAuthorByName(authorRequest.Name);

            if (existing.Name == null)
            {
                return NotFound(existing);
            }

            _authorServices.UpdateAuthor(authorRequest);
            return Ok(existing);
        }
        [HttpDelete(nameof(Delete))]
        public IActionResult? Delete([FromBody] int authorId)
        {
            return Ok(_authorServices.DeleteAuthor(authorId));
        }
    }
}
