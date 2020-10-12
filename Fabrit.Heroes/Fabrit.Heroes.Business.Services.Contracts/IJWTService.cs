using Fabrit.Heroes.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IJWTService
    {
        string GenerateAuthenticationJWT(User user);
        string GenerateRegisterJWT(User user);
        string DecodeRegisterToken(string token);
        int DecodeAuthenticationJWT(string token);
    }
}
