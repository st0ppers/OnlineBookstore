using BookStore.Models.Models;


namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        public Guid Id { get; set; }
        public IEnumerable<Person> GetAllPeople();
        public Person GetById(int id);
        public Person AddPerson(Person person);
        public Person UpdatePerson(Person person);
        public Person DeletePerson(int authroId);
        public Guid GetGuidId();
    }
}
