﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Contracts.V1.Requests
{
    public class LoginUserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
