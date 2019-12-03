using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Store.App.Models;

namespace Store.App.Services
{
    public class UsersService : IUsersService
    {
        public async Task<string> LoginAsync(UserViewModel userViewModel)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync("http://localhost:19496/api/v1/users/login", userViewModel);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var authSuccess = await response.Content.ReadAsAsync<AuthSuccess>();
            return authSuccess.Token;
        }
    }
}
