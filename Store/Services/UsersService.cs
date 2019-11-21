using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Helpers;
using Store.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppSettings appSettings;

        public UsersService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }

        public async Task<UserResult> AddAsync(string email, string password)
        {
            var userResult = new UserResult();
            var user = new IdentityUser { UserName = email };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                userResult.Token = await GenerateToken(email);
            }
            else
            {
                userResult.Errors = result.Errors.Select(x => x.Description);
            }

            return userResult;
        }

        public async Task<UserResult> LoginAsync(string email, string password)
        {
            var userResult = new UserResult();
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
            {
                userResult.Token = await GenerateToken(email);
            }
            else
            {
                userResult.Errors = new[] { "Login lub hasło jest niepoprawne" };
            }

            return userResult;
        }

        private async Task<string> GenerateToken(string email)
        {
            var user = await userManager.FindByNameAsync(email);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
