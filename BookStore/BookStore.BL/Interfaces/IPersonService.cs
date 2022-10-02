using BookStore.Models.Models;


namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAllPeople();
        public Person GetById(int id);
        public Person AddPerson(Person person);
        public Person UpdatePerson(Person person);
        public Person DeletePerson(int personId);
    }
}
