using BookStore.Models.Models;


namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {

        private static List<Person> _persons;
        public Guid Id { get; set; }
        public Person GetById(int id);
        public Person AddUser(Person person);
        public Person UpdateUser(Person person);
        public Person DeleteUser(int authroId);
        public Guid GetGuidId();
    }
}
