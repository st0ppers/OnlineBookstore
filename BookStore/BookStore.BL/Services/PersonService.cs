using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    // business layer
    public class PersonService : IPersonService
    {
        private readonly IPersonRepo _personRepository;
        public Guid Id { get; set; }
        public PersonService(IPersonRepo ipr)
        {
            _personRepository = ipr;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }
        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public Person AddPerson(Person person)
        {
            _personRepository.AddPerson(person);
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            return _personRepository.UpdatePerson(person);
        }

        public Person DeletePerson(int personId)
        {
            return _personRepository.DeletePerson(personId);
        }

        public Guid GetGuidId()
        {
            return _personRepository.GetGuidId();
        }
    }
}