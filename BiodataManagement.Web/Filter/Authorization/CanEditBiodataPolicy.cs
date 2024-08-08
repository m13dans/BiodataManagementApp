using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BiodataManagement.Service.BiodataService;

namespace BiodataManagement.Authorization;

public static class CanEditBiodataPolicy
{
    public class CanEditBiodataRequirement : IAuthorizationRequirement
    {
    }

    public class Handler : AuthorizationHandler<CanEditBiodataRequirement, int>
    {
        private readonly IBiodataRepository _biodataRepository;
        public Handler(IBiodataRepository biodataRepository)
        {
            _biodataRepository = biodataRepository;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CanEditBiodataRequirement requirement,
            int resource)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userBiodataId = context.User.FindFirstValue("BiodataId");
            if (int.Parse(userBiodataId) == resource)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }

}
