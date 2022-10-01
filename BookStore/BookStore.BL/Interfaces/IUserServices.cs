using BookStore.Models.Models;
namespace BookStore.BL.Interfaces
{
    public interface IUserServices
    {
        public IEnumerable<User> GetAllUsers();
        public User GetById(int id);
        public User AddUser(User user);
        public User UpdateUser(User user);
        public User DeleteUser(int userid);
    }
}
