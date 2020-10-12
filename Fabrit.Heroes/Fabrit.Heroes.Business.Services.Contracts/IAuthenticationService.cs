using Fabrit.Heroes.Data.Business.Authentication;
using Fabrit.Heroes.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model);
        Task<AuthenticateResponseDto> GetUser(string token);
    }
}
