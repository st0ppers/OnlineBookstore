using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class PersonRepo : IPersonRepo
    {
        private static List<Person?> _persons = new List<Person?>()
        {
            new Person()
            {
                Id = 1,
                Name = "pesho",
                Age = 20,
            },
            new Person()
            {
                Id = 2,
                Name = "gosho",
                Age = 24,
            },
            new Person()
            {
                Id = 3,
                Name = "tosho",
                Age = 31,
            }
        };

        public IEnumerable<Person> GetAllPeople()
        {
            return _persons;
        }
        public Person GetById(int id)
        {
            return _persons.FirstOrDefault(x => x.Id == id);
        }

        public Person AddPerson(Person person)
        {
            if (person == null)
            {
                return null;
            }
            _persons.Add(person);
            return person;
        }

        public Person GetByName(string name)
        {
            return _persons.FirstOrDefault(x => x.Name == name);
        }
        public Person UpdatePerson(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Id == person.Id);
            if (existingPerson == null)
            {
                return null;
            }

            _persons.Remove(existingPerson);
            _persons.Add(person);
            return person;
        }

        public Person DeletePerson(int personId)
        {
            var input = _persons.FirstOrDefault(x => x.Id == personId);
            _persons.Remove(input);
            return input;
        }
    }
}
