using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        // user interface
        private static IPersonService _personSerice;
        public PersonController(IPersonService ips)
        {
            _personSerice = ips;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return _personSerice.GetAllPeople();
        }

        [HttpGet(nameof(GetByID))]
        public Person GetByID(int id)
        {
            return _personSerice.GetById(id);
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _personSerice.GetGuidId();
        }

        [HttpPost(nameof(Add))]
        public Person Add([FromQuery] Person input)
        {
            return _personSerice.AddPerson(input);
        }
        [HttpPut(nameof(Update))]
        public Person? Update([FromBody] Person person)
        {
            return _personSerice.UpdatePerson(person);
        }
        [HttpDelete(nameof(Delete))]
        public Person? Delete([FromBody] Person person)
        {
            return _personSerice.UpdatePerson(person);
        }

    }
}
