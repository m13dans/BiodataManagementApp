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
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BiodataOwnerOrAdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
                context.Succeed(requirement);

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                context.Fail();

            var biodataId = await _biodataRepository.GetBiodataOwnerId(userId);

            if (userId == biodataId)
                context.Succeed(requirement);

            var userEmail = context.User.FindFirstValue(ClaimTypes.Email);
            var validateBiodataOwner = await _biodataRepository.ValidateBiodataOwner(userEmail);
            if (validateBiodataOwner)
                context.Succeed(requirement);
        }
    }

}
