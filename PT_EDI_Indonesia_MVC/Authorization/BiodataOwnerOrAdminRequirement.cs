using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PT_EDI_Indonesia_MVC.Data.Identity;
using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.BiodataService;

namespace PT_EDI_Indonesia_MVC.Authorization;

public static class BiodataOwnerOrAdminPolicy
{
    public class BiodataOwnerOrAdminRequirement : IAuthorizationRequirement
    {
    }

    public class Handler : AuthorizationHandler<BiodataOwnerOrAdminRequirement, Biodata>
    {
        private readonly IBiodataRepository _biodataRepository;
        public Handler(IBiodataRepository biodataRepository)
        {
            _biodataRepository = biodataRepository;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            BiodataOwnerOrAdminRequirement requirement,
            Biodata resource)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == resource.UserId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }

}