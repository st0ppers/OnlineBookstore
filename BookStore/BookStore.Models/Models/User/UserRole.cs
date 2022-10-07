using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Models.User
{
    public class UserRole : IdentityRole
    {
        public int UserId { get; set; }

    }
}