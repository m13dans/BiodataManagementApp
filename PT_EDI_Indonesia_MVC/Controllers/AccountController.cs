using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.Accounts;
using PT_EDI_Indonesia_MVC.Service.Accounts.AccountService;

namespace PT_EDI_Indonesia_MVC.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AccountRepository _accountRepo;
        private readonly AccountService _service;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
        AccountRepository accountRepo, AccountService service)
        {
            _service = service;
            _accountRepo = accountRepo;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("SignUp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignupDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = model.MapSignUpToUser();

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

            return RedirectToAction(nameof(Login), "Account");
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



            // if (User.IsInRole("Admin"))
            // {
            //     return RedirectToAction(nameof(BiodataController.Index), "Biodata");
            // }

            if (result.Succeeded)
            {
                var userIdInDatabase = await _accountRepo.GetUserIdAndEmailAsync(User.FindFirstValue(ClaimTypes.Email));
                if (userIdInDatabase is null)
                {
                    return RedirectToLocal(returnUrl);
                }
                var claims = new List<Claim>
                {
                    new Claim("BioId", userIdInDatabase.Id.ToString())
                };

                var appIdentity = new ClaimsIdentity(claims);
                User.AddIdentity(appIdentity);

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