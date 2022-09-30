using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IPersonRepo
    {
        private static List<Person> _persons;
        public Guid Id { get; set; }
        public Person GetById(int id);
        public Person AddUser(Person person);
        public Person UpdateUser(Person person);
        public Person DeleteUser(int personId);
        public Guid GetGuidId();
    }
}
