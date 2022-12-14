using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IPersonRepo
    {
        public IEnumerable<Person> GetAllPeople();
        public Person GetById(int id);
        public Person AddPerson(Person person);
        public Person UpdatePerson(Person person);
        public Person GetByName(string name);
        public Person DeletePerson(int personId);
    }
}
