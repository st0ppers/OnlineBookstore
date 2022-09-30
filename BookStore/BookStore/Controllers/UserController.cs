using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.DL.Interface;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        // user interface
        private static IUserRepository _userRepo;
        public static readonly List<User> Models = new List<User>()
        {
            new ()
            {
                Id = 1,
                Name = "gosho"
            },
            new ()
            {
                Id = 2,
                Name = "pesho"
            },
            new ()
            {
                Id = 3,
                Name = "tosho"
            }
        };

        public UserController(IUserRepository ur)
        {
            _userRepo = ur;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<User> Get()
        {
            return Models;
        }

        [HttpGet(nameof(GetByID))]
        public IEnumerable<User> GetByID(int id)
        {
            return Models.Where(x => x.Id == id);
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _userRepo.GetGuidId();
        }

        [HttpPost(nameof(Add))]
        public bool Add([FromQuery] User input)
        {
            Models.Add(input);
            return true;
        }
        [HttpPut(nameof(Update))]
        public User? Update([FromBody] User user)
        {
            return _userRepo.UpdateUser(user);
        }
        [HttpDelete(nameof(Delete))]
        public User? Delete([FromBody] User user)
        {
            return _userRepo.UpdateUser(user);
        }
        
    }
}
