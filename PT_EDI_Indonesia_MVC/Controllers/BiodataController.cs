using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Data.IRepository;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Data.Seed;

namespace PT_EDI_Indonesia_MVC.Controllers
{
    [Authorize]
    public class BiodataController : Controller
    {
        private readonly ILogger<BiodataController> _logger;
        private readonly IBiodataRepository _bioRepo;

        private readonly AccountRepository _accountRepo;

        public BiodataController(ILogger<BiodataController> logger, IBiodataRepository bioRepo,
        AccountRepository accounRepo)
        {
            _accountRepo = accounRepo;
            _bioRepo = bioRepo;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var bioVm = await _bioRepo.GetBiodatasAsync();
                return View(bioVm);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Error();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> GenerateData([FromServices] GenerateData generateData)
        {
            var result = await generateData.SubmitBiodata();

            return Json(result);
        }


        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Detail(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (User.IsInRole("Admin") & id != 0)
            {
                var bio = await _bioRepo.GetBiodataAsync(id);
                return View(bio);
            }

            var biodata = await _bioRepo.GetBiodataWithEmailAsync(email);

            if (biodata == null && User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Biodata");
            }

            if (biodata is null)
            {
                return RedirectToAction("Create", "Biodata");
            }

            return View(biodata);
        }


        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var biodata = await _bioRepo.GetBiodataWithEmailAsync(email);

            if (biodata is not null)
            {
                return RedirectToAction("Update");
            }

            return View();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> Create(Biodata biodata)
        {
            var result = await _bioRepo.CreateBiodataAsync(biodata);
            if (result is false)
            {
                return Error();
            }
            return RedirectToAction("Detail", "Biodata", new { id = biodata.Id });
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Update(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (User.IsInRole("Admin"))
            {
                var bio = await _bioRepo.GetBiodataAsync(id);
                return View(bio);
            }

            var biodata = await _bioRepo.GetBiodataWithEmailAsync(email);

            if (biodata != null)
            {
                return View(biodata);
            }

            return RedirectToAction("create");

        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> Update(Biodata biodata)
        {
            var result = await _bioRepo.UpdateBiodataAsync(biodata);
            if (result is false)
            {
                return Error();
            }
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bioRepo.DeleteBiodataAsync(id);
            if (result is false)
            {
                return Error();
            }
            return RedirectToAction("Index", "Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}