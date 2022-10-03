using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAllPeople();
        public Person GetById(int id);
        public Person GetByName(string name);
        public AddPersonResponse AddPerson(AddPersonRequest personRequest);
        public AddPersonResponse UpdatePerson(AddPersonRequest personRequest);
        public Person DeletePerson(int personId);
    }
}
