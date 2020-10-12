using System.Threading.Tasks;
using AutoMapper;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Authentication;
using Fabrit.Heroes.Data.Business.User;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto userData)
        {
            await _userService.CreateUser(userData);
            return Created(Constants.HTTP_CREATED, userData);
        }

        [HttpPost("userToken")]
        [AllowAnonymous]
        public async Task<IActionResult> ActivateUserAccount([FromQuery] string token)
        {
            await _userService.ActivateAccount(token);
            return Ok();
        }

        [HttpPut("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string password, [FromQuery] string newPassword, [FromQuery] bool isForgotten)
        {
            await _userService.ResetPassword(email, isForgotten, password, newPassword);
            return Ok();
        }

        [AuthorizeUserCustom(RoleType.General)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AuthorizeUserCustom(RoleType.General)]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userData)
        {
            await _userService.UpdateUser(userData);
            return Created(Constants.HTTP_UPDATED, userData);
        }
    }
}
