using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Password
{
    public interface IHashingManager
    {
        string GetHashedPassword(string password);
        int GetPasswordMinLength();
    }
}
