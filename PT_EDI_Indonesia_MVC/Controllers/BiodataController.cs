using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Data.IRepository;
using PT_EDI_Indonesia_MVC.Data.Seed;

namespace PT_EDI_Indonesia_MVC.Controllers
{
    [Authorize]
    public class BiodataController : Controller
    {
        private readonly ILogger<BiodataController> _logger;
        private readonly IBiodataRepository _bioRepo;

        public BiodataController(ILogger<BiodataController> logger, IBiodataRepository bioRepo)
        {
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
        public IActionResult Detail()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int id)
        {
            var biodata = await _bioRepo.GetBiodataAsync(id);
            return View(biodata);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Biodata biodata)
        {
            var result = await _bioRepo.UpdateBiodataAsync(biodata);
            if (result is false)
            {
                return Error();
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LoadListPendidikan(int id)
        {
            var listPendidikan = await _bioRepo.GetPendidikansAsync(id);
            return View(listPendidikan);
        }
        public async Task<IActionResult> LoadPendidikanPartial(int biodataId)
        {
            var listPendidikan = await _bioRepo.GetPendidikansAsync(biodataId);
            return PartialView("_PendidikanTerakhir", listPendidikan);
        }

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