using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Data.Identity;

namespace PT_EDI_Indonesia_MVC.Controllers;

public static class ControllerHelper
{
    public static async Task<bool> CreateUserWithRole(
       AppUser user,
       UserManager<AppUser> userManager,
       string password,
       ControllerBase controller,
       string role = "User")
    {
        IdentityResult result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                    continue;
                controller.ModelState.TryAddModelError(error.Code, error.Description);
            }

            return false;
        }

        await userManager.AddToRoleAsync(user, role);
        return true;
    }

    public static async Task<bool> AssignDefaultRole(string email, UserManager<AppUser> userManager)
    {
        AppUser appUser = await userManager.FindByNameAsync(email);
        var result = email switch
        {
            "admin@test.com" => await userManager.AddToRoleAsync(appUser, "Admin"),
            _ => await userManager.AddToRoleAsync(appUser, "User")
        };

        return result.Succeeded;
    }
}
