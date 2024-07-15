using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Data.Identity;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Service.Accounts;
using PT_EDI_Indonesia_MVC.Service.Accounts.AccountService;
using PT_EDI_Indonesia_MVC.Service.AccountService;
using static PT_EDI_Indonesia_MVC.Controllers.ControllerHelper;

namespace PT_EDI_Indonesia_MVC.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAccountRepository _accountRepo;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IAccountRepository accountRepo)
    {
        _accountRepo = accountRepo;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost("signup")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(SignupDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        AppUser user = model.MapSignUpToUser();

        bool result = await CreateUserWithRole(user, _userManager, model.Password, this);

        if (result is false)
            return View(model);

        return RedirectToAction(nameof(Login), "account");
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDTO userModel,
        string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(userModel);
        }

        var result = await _signInManager.PasswordSignInAsync(
            userModel.Email,
            userModel.Password,
            userModel.RememberMe,
            false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid UserName or Password");
            return View(userModel);
        }

        await AssignDefaultRole(userModel.Email, _userManager);
        return RedirectToLocal(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}