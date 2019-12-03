using Microsoft.AspNetCore.Mvc;
using Store.App.Models;
using Store.App.Services;
using System.Threading.Tasks;

namespace Store.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            var token = await usersService.LoginAsync(user);

            if(string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(Login));
            }

            Response.Cookies.Append("api_token", token);

            return RedirectToAction("Index", "Products");
        }
    }
}