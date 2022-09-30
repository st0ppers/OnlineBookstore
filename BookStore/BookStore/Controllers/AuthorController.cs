using BookStore.Models;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.DL.Interface;

namespace BookStore.Controllers
{
    public class AuthorController : ControllerBase
    {
        private static IAuthorRepo _authorRepo;
        public static readonly List<Author> Authors = new List<Author>()
        {
            new ()
            {
                Id = 1,
                Name = "gosho",
                Age = 22,
                DateOfBirth = DateTime.Now,
                Nickname = "Gopeto"

            },
            new ()
            {
                Id = 2,
                Name = "pesho",
                Age = 25,
                DateOfBirth = DateTime.MaxValue,
                Nickname = "Peshkata"
            },
            new ()
            {
                Id = 3,
                Name = "tosho",
                Age = 42,
                DateOfBirth = DateTime.MinValue,
                Nickname = "Topkata"
            }
        };

        public AuthorController(IAuthorRepo ur)
        {
            _authorRepo = ur;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Author> Get()
        {
            return Authors;
        }

        [HttpGet(nameof(GetByID))]
        public IEnumerable<Author> GetByID(int id)
        {
            return Authors.Where(x => x.Id == id);
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _authorRepo.GetGuidId();
        }

        [HttpPost(nameof(Add))]
        public bool Add([FromQuery] Author input)
        {
            Authors.Add(input);
            return true;
        }
        [HttpPut(nameof(Update))]
        public Author? Update([FromBody] Author author)
        {
            return _authorRepo.UpdateUser(author);
        }
        [HttpDelete(nameof(Delete))]
        public Author? Delete([FromBody] Author author)
        {
            return _authorRepo.UpdateUser(author);
        }
    }
}
