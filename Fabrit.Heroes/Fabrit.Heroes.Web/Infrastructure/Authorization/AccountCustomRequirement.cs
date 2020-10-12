using Fabrit.Heroes.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Web.Infrastructure.Policy
{
    public class AccountCustomRequirement : IAuthorizationRequirement
    {
        public RoleType RequiredRole { get; set; }

        public AccountCustomRequirement(RoleType role)
        {
            RequiredRole = role;
        }
    }
}
