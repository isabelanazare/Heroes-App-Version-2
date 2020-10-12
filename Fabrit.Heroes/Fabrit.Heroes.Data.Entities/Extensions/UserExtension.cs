using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Fabrit.Heroes.Data.Entities.Extensions
{
    public static class UserExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            return !string.IsNullOrEmpty(userId) && int.TryParse(userId, out int id) ? id : 0;
        }
        public static int GetRoleId(this ClaimsPrincipal user)
        {
            var roleId = user.Claims.FirstOrDefault(c => c.Type == "roleId")?.Value;
            return !string.IsNullOrEmpty(roleId) && int.TryParse(roleId, out int id) ? id : 0;
        }
    }
}
