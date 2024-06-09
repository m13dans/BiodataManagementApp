using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Data.IRepository;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Data.Seed;

namespace PT_EDI_Indonesia_MVC.Controllers;

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
            _logger.LogError(ex.Message);
            return NotFound();
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
        if (User.IsInRole("Admin"))
        {
            var bio = await _bioRepo.GetBiodataAsync(id);

            return View(bio);
        }

        var userBioId = User.FindFirstValue("BioId");
        if (userBioId is not null)
        {
            var bioData = await _bioRepo.GetBiodataAsync(int.Parse(userBioId));
            return View(bioData);
        }

        return RedirectToAction("Index", "Home");
    }


    [Authorize(Roles = "Admin, User")]
    public IActionResult Create()
    {
        var userBioId = User.FindFirstValue("BioId");


        if (userBioId is not null)
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
        if (User.IsInRole("Admin"))
        {
            var bio = await _bioRepo.GetBiodataAsync(id);
            return View(bio);
        }

        var userBioId = User.FindFirstValue("BioId");
        if (userBioId != null)
        {
            return View(userBioId);
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