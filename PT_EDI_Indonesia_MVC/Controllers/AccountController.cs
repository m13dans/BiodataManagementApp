using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Data.Repository;

namespace PT_EDI_Indonesia_MVC.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AccountRepository _accountRepo;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
        AccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel,
            string? returnUrl = null)
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

            if (result.Succeeded)
            {
                var userBioId = await _accountRepo.GetUserBioIdAsync(userModel.Email);
                if (userBioId is 0)
                {
                    return RedirectToLocal(returnUrl);
                }
                var claims = new List<Claim>
                {
                    new("BioId", userBioId.ToString())
                };

                var user = await _userManager.FindByEmailAsync(userModel.Email);
                var addClaimResult = await _userManager.AddClaimsAsync(user, claims);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }

        }
        // [HttpPost("Login")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Login(UserLoginModel userModel, string? returnUrl = null)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(userModel);
        //     }

        //     var user = await _userManager.FindByEmailAsync(userModel.Email);

        //     if (user != null &&
        //         await _userManager.CheckPasswordAsync(user, userModel.Password))
        //     {
        //         var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        //         identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //         identity.AddClaim(new Claim(ClaimTypes.Name, user.NamaLengkap));

        //         var roles = await _userManager.GetRolesAsync(user);

        //         foreach (var role in roles)
        //         {
        //             identity.AddClaim(new Claim(ClaimTypes.Role, role));
        //         }

        //         await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
        //             new ClaimsPrincipal(identity));

        //         return RedirectToLocal(returnUrl);
        //     }
        //     else
        //     {
        //         ModelState.AddModelError("", "Invalid UserName or Password");
        //         return View(userModel);
        //     }
        // }


        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("SignUp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserSignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                NamaLengkap = model.NamaLengkap,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(model);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(AccountController.Login), "Account");
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
}