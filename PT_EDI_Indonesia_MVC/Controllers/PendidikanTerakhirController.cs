using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.BiodataService;

namespace PT_EDI_Indonesia_MVC.Controllers
{
    [Authorize]
    public class PendidikanTerakhirController : Controller
    {
        private readonly ILogger<PendidikanTerakhirController> _logger;
        private readonly PendidikanTerakhirRepository _pendidikanRepo;
        private readonly IBiodataRepository _biodataRepository;

        public PendidikanTerakhirController(ILogger<PendidikanTerakhirController> logger,
            PendidikanTerakhirRepository pendidikanRepo,
            IBiodataRepository biodataRepository
        )
        {
            _biodataRepository = biodataRepository;
            _pendidikanRepo = pendidikanRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var pendidikan = await _pendidikanRepo.GetPendidikanWithEmailAsync(email);

            if (pendidikan.Count > 3)
            {
                return RedirectToAction("Update");
            }

            return View();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> Create(PendidikanTerakhir pendidikan)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var currentUserId = await _biodataRepository.GetCurrentUserId(email);
            var result = await _pendidikanRepo.CreatePendidikanAsync(pendidikan, currentUserId);

            if (result is false)
            {
                return Error();
            }
            return RedirectToAction("Detail", "Biodata");
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Update(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (User.IsInRole("Admin"))
            {
                var pend = await _pendidikanRepo.GetPendidikanAsync(id);
                return View(pend);
            }

            var pendidikans = await _pendidikanRepo.GetPendidikanWithEmailAsync(email);

            if (pendidikans.Count > 0)
            {
                return View(pendidikans);
            }

            return RedirectToAction("create");

        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> Update(PendidikanTerakhir pendidikan)
        {
            var result = await _pendidikanRepo.UpdatePendidikanAsync(pendidikan);
            if (result is false)
            {
                return Error();
            }
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pendidikanRepo.DeletePendidikanAsync(id);
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