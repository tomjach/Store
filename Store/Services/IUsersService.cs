using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface IUsersService
    {
        Task<UserResult> AddAsync(string email, string password);
        Task<UserResult> LoginAsync(string email, string password);
    }
}
