using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Store.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task AddAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email };
            var result = await userManager.CreateAsync(user, password);
        }

        public async Task LoginAsync(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        }
    }
}
