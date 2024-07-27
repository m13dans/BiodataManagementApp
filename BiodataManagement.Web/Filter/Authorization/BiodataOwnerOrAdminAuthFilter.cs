using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Service.BiodataService;

namespace BiodataManagement.Authorization;

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
            var userEmail = context.User.FindFirstValue(ClaimTypes.Email);
            if (userId == resource.UserId || userEmail == resource.Email)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }

}
