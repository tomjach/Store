using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Helpers
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public string Id
        {
            get
            {
                return httpContextAccessor.HttpContext.User.Identity.Name;
            }
        }

        public User(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
