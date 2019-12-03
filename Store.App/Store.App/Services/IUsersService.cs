using Store.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.App.Services
{
    public interface IUsersService
    {
        Task<string> LoginAsync(UserViewModel userViewModel);
    }
}
