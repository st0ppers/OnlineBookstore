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
        public List<Person> Persons { get; set; }

        public PersonService(IPersonRepo ipr)
        {
            _personRepository = ipr;
        }
        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public Person AddUser(Person person)
        {
            _personRepository.AddUser(person);
            return person;
        }

        public Person UpdateUser(Person person)
        {
            return _personRepository.UpdateUser(person);
        }

        public Person DeleteUser(int personId)
        {
            return _personRepository.DeleteUser(personId);
        }

        public Guid GetGuidId()
        {
            return _personRepository.GetGuidId();
        }
    }
}