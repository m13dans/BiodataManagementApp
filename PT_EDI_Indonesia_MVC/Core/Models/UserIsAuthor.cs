using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PT_EDI_Indonesia_MVC.Core.Models
{
    public class UserIsAuthorRequirement : IAuthorizationRequirement
    {

    }

    public class UserIsAuthorAuthorizationHandler : AuthorizationHandler<UserIsAuthorRequirement, Biodata>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserIsAuthorRequirement requirement,
            Biodata resource)
        {
            if (context.User.FindFirstValue("BioId") == resource.Id.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}