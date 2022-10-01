using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>()
        {
            new()
            {
                Id = 1,
                Name = "Gosho",
                Age = 20
            },
            new()
            {
                Id = 2,
                Name = "Pesho",
                Age = 21
            },
            new()
            {
                Id = 3,
                Name = "Tosho",
                Age = 2
            }
        };
        public Guid Id { get; set; }

        public UserRepository()
        {
            Id = Guid.NewGuid();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }


        public Guid GetGuidId()
        {
            return Id;
        }
        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
        public User AddUser(User user)
        {
            try
            {
                _users.Add(user);
            }
            catch (Exception e)
            {
                return null;
            }
            return user;
        }
        public User? UpdateUser(User user)
        {
            var existinUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (existinUser == null) return null;

            _users.Remove(existinUser);
            _users.Add(user);
            return user;
        }
        public User DeleteUser(int userId)
        {

            var user = _users.FirstOrDefault(x => x.Id == userId);
            _users.Remove(user);
            return user;
        }
    }
}