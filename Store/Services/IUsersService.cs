using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface IUsersService
    {
        Task AddAsync(string email, string password);
        Task LoginAsync(string email, string password);
    }
}
