using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost(ApiRoutes.Users.Add)]
        public async Task<IActionResult> Add([FromBody]NewUserRequest userRequest)
        {
            var userResult = await usersService.AddAsync(userRequest.Email, userRequest.Password);

            if (userResult.Errors != null && userResult.Errors.Any())
            {
                return BadRequest(new AuthFailedResponse { Errors = userResult.Errors });
            }

            return Ok(new AuthSuccessResponse { Token = userResult.Token });
        }

        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody]LoginUserRequest userRequest)
        {
            var userResult = await usersService.LoginAsync(userRequest.Email, userRequest.Password);

            if (userResult.Errors != null && userResult.Errors.Any())
            {
                return BadRequest(new AuthFailedResponse { Errors = userResult.Errors });
            }

            return Ok(new AuthSuccessResponse { Token = userResult.Token });
        }
    }
}