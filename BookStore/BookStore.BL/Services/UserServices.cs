using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class UserServices : IUserServices
    {
        public readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public User UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        public User DeleteUser(int userid)
        {
            return _userRepository.DeleteUser(userid);
        }
    }
}
