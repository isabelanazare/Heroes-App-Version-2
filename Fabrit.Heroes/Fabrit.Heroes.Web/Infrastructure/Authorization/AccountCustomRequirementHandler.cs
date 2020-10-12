using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Entities.Extensions;
using Fabrit.Heroes.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Web.Infrastructure.Policy
{
    public class AccountCustomRequirementHandler : AuthorizationHandler<AccountCustomRequirement>
    {
        private readonly IUserService _userService;
        public AccountCustomRequirementHandler(IUserService userService)
        {
            _userService = userService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountCustomRequirement requirement)
        {
            var userId = context.User.GetId();
            User user = _userService.FindById(userId).Result;

            if (user == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.RequiredRole == RoleType.General) context.Succeed(requirement);

            if (user.IsActivated && (context.User.GetRoleId() == (int)requirement.RequiredRole)) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
