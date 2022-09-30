using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.DL.Interface;
using System.Xml.Linq;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        // user interface
        private static IPersonService _personSerice;
        public static readonly List<Person> Persons = new List<Person>()
        {
            new ()
            {
                Id = 1,
                Name = "gosho",
                Age = 20,
                BirthOfDate = DateTime.Now,
            },
            new ()
            {
                Id = 2,
                Name = "pesho",
                Age = 26,
                BirthOfDate = DateTime.Now,
            },
            new ()
            {
                Id = 3,
                Name = "tosho",
                Age = 60,
                BirthOfDate = DateTime.Now,
            }
        };

        public PersonController(IPersonService ips)
        {
            _personSerice = ips;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return Persons;
        }

        [HttpGet(nameof(GetByID))]
        public IEnumerable<Person> GetByID(int id)
        {
            return Persons.Where(x => x.Id == id);
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _personSerice.GetGuidId();
        }

        [HttpPost(nameof(Add))]
        public bool Add([FromQuery] Person input)
        {
            Persons.Add(input);
            return true;
        }
        [HttpPut(nameof(Update))]
        public Person? Update([FromBody] Person user)
        {
            return _personSerice.UpdateUser(user);
        }
        [HttpDelete(nameof(Delete))]
        public Person? Delete([FromBody] Person user)
        {
            return _personSerice.UpdateUser(user);
        }

    }
}
