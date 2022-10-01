using BookStore.BL.Interfaces;
using BookStore.Models.Models;
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
        public IEnumerable<Author> Get()
        {
            return _authorServices.GetAllAuthors();
        }

        [HttpGet(nameof(GetByID))]
        public Author GetByID(int id)
        {
            return _authorServices.GetById(id);
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _authorServices.GetGuidId();
        }

        [HttpPost(nameof(Add))]
        public Author Add([FromQuery] Author input)
        {
            return _authorServices.AddAuthor(input);
        }
        [HttpPut(nameof(Update))]
        public Author? Update([FromBody] Author author)
        {
            return _authorServices.UpdateAuthor(author);
        }
        [HttpDelete(nameof(Delete))]
        public Author? Delete([FromBody] int authorId)
        {
            return _authorServices.DeleteAuthor(authorId);
        }
    }
}
