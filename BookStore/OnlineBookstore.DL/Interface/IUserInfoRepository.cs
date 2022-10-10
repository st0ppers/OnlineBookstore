using BookStore.Models.Models.User;

namespace OnlineBookstore.DL.Interface
{
    public interface IUserInfoRepository
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
