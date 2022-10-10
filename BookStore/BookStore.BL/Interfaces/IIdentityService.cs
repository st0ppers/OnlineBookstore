using BookStore.Models.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BookStore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(UserInfo user);
        Task<UserInfo?> CheckUserAndPassword(string userNmae, string Password);
        public Task<IEnumerable<string>> GetAllRoles(UserInfo user);
    }
}
