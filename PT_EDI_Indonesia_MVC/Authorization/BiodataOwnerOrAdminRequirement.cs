using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PT_EDI_Indonesia_MVC.Service.BiodataService;

namespace PT_EDI_Indonesia_MVC.Authorization;

public static class BiodataOwnerOrAdminPolicy
{
    public class BiodataOwnerOrAdminRequirement : IAuthorizationRequirement
    {
    }

    public class Handler : AuthorizationHandler<BiodataOwnerOrAdminRequirement>
    {
        private readonly IBiodataRepository _biodataRepository;
        public Handler(IBiodataRepository biodataRepository)
        {
            _biodataRepository = biodataRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BiodataOwnerOrAdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
                context.Succeed(requirement);

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                context.Fail(requirement);
            }
            var biodataId = _biodataRepository.GetBiodataOwnerId()

            if (context.User.FindFirst(ClaimTypes.Email) == )
        }
    }

}
