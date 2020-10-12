using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Authentication;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto model)
        {
            return Ok(await _authenticationService.Authenticate(model));
        }

        [HttpPost("isAuthenticated")]
        public async Task<IActionResult> CheckIfAuthenticated([FromBody] TokenDto token)
        {
            return Ok(await _authenticationService.GetUser(token.Token));
        }
    }
}
