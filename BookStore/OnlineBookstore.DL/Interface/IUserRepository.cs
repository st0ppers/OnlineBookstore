using BookStore.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IUserRepository
    {
        private static List<User> _users;
        public Guid Id { get; set; }
        public IEnumerable<User> GetAllUsers();
        public User GetById(int id);
        public User AddUser(User user);
        public User UpdateUser(User user);
        public User DeleteUser(int userId);
        public Guid GetGuidId();

    }
}
