using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Authentication;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;
using Fabrit.Heroes.Infrastructure.Common.Password;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fabrit.Heroes.Data.Entities.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Authentication;
using Fabrit.Heroes.Infrastructure.Common;

namespace Fabrit.Heroes.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly HeroesDbContext _context;
        private readonly IHashingManager _hashingManager;
        private readonly IJWTService _jwtService;

        public AuthenticationService(HeroesDbContext context, IHashingManager hashingManager, IOptions<AppSettings> appSettings, IJWTService jwtService)
        {
            _context = context;
            _hashingManager = hashingManager;
            _appSettings = appSettings.Value;
            _jwtService = jwtService;
        }

        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model)
        {
            var user = await _context.Users.Include(user => user.Role).FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == _hashingManager.GetHashedPassword(model.Password));

            if (user == null)
            {
                throw new AuthenticationException("Username or password is incorrect");
            }

            if (!user.IsActivated)
            {
                throw new AuthenticationException("Please activate your account first!");
            }

            if ((user.WasPasswordChanged && user.WasPasswordForgotten) || (!user.WasPasswordChanged && !user.WasPasswordForgotten))
            {
                user.WasPasswordChanged = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                var token = _jwtService.GenerateAuthenticationJWT(user);
                var authenticateResponseDto = new AuthenticateResponseDto(user, token);

                return authenticateResponseDto != null
                    ? authenticateResponseDto
                    : throw new AuthenticationException("Username or password is incorrect");
            }

            throw new AuthenticationException("Username or password is incorrect");
        }

        public async Task<AuthenticateResponseDto> GetUser(string token)
        {
            var user = await _context.Users.Include(user => user.Role).FirstAsync(user => user.Id == _jwtService.DecodeAuthenticationJWT(token));
            return new AuthenticateResponseDto(user, token);
        }
    }
}
