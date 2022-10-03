using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

        [HttpGet(nameof(GetById))]
        public Person GetById(int id)
        {
            return _personSerice.GetById(id);
        }

        [HttpPost(nameof(Add))]
        public IActionResult Add([FromBody] AddPersonRequest personRequest)
        {
            var result = _personSerice.GetByName(personRequest.Name);

            if (result.Name == null)
            {
                return NotFound();
            }

            _personSerice.AddPerson(personRequest);
            return Ok(result);

        }
        [HttpPut(nameof(Update))]
        public IActionResult? Update([FromBody] AddPersonRequest personRequest)
        {
            var result = _personSerice.GetByName(personRequest.Name);

            if (result.Name == null)
            {
                return null;
            }

            _personSerice.UpdatePerson(personRequest);
            return Ok(result);

        }
        [HttpDelete(nameof(Delete))]
        public Person? Delete([FromBody] int personId)
        {
            return _personSerice.DeletePerson(personId);
        }

    }
}
