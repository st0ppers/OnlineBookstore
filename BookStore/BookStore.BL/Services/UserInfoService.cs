using BookStore.BL.Interfaces;
using BookStore.Models.Models.User;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _infoRepository;

        public UserInfoService(IUserInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            return await _infoRepository.GetUserInfoAsync(email, password);
        }
    }
}
