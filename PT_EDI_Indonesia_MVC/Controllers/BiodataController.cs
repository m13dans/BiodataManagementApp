using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Data.Seed;
using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.Accounts;
using PT_EDI_Indonesia_MVC.Service.BiodataService;

namespace PT_EDI_Indonesia_MVC.Controllers;

[Authorize]
[Route("biodata")]
public class BiodataController : Controller
{
    private readonly ILogger<BiodataController> _logger;
    private readonly IBiodataRepository _bioRepo;

    private readonly IAccountRepository _accountRepo;

    public BiodataController(ILogger<BiodataController> logger, IBiodataRepository bioRepo,
    IAccountRepository accounRepo)
    {
        _accountRepo = accounRepo;
        _bioRepo = bioRepo;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        ErrorOr<List<BiodataDTO>> errorOrResult = await _bioRepo.GetBiodataListAsync();

        return errorOrResult.MatchFirst(
            onValue: result => View(result),
            onFirstError: error =>
            {
                _logger.LogWarning($"{error.Code} {error.Description}");
                return View();
            });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<JsonResult> GenerateData([FromServices] GenerateData generateData)
    {
        var result = await generateData.SubmitBiodata();

        // var result = await _bioRepo.GetBiodataListAsync();

        return Json(result);
    }

    [HttpGet("displaydata")]
    public async Task<IActionResult> DisplayData()
    {
        var result = await _bioRepo.GetBiodataListAsync();

        return PartialView("_BiodataListPartial", result.Value);
    }


    [Authorize(Policy = "BiodataOwner")]
    [HttpGet("detail/{id:int?}")]
    public async Task<IActionResult> Detail(int? id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (id is null)
        {
            var result = await _bioRepo.GetBiodataWithEmailAsync(email);
            return View(result.Value);
        }

        var biodata = await _bioRepo.GetBiodataByIdAsync(id.Value);

        if (biodata.IsError)
        {
            var error = biodata.FirstError;
            return error.Code switch
            {
                "Biodata.NotFound" => NotFound(),
                _ => BadRequest()
            };
        }

        if (biodata.Value.Email != email)
        {
            return NotFound();
        }

        return View(biodata.Value);
    }


    [Authorize(Roles = "Admin, User")]
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var biodata = await _bioRepo.GetBiodataWithUserId(userId);

        if (!biodata.IsError)
        {
            return RedirectToAction("Update");
        }

        return View();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    public async Task<IActionResult> Create(Biodata biodata)
    {
        if (!ModelState.IsValid)
            return View(biodata);

        biodata.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await _bioRepo.CreateBiodataAsync(biodata);
        if (result is false)
        {
            return Error();
        }
        return RedirectToAction("Detail", "Biodata", new { id = biodata.Id });
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("update")]
    public async Task<IActionResult> Update(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (User.IsInRole("Admin"))
        {
            var bio = await _bioRepo.GetBiodataByIdAsync(id);
            return View(bio);
        }

        var biodata = await _bioRepo.GetBiodataWithEmailAsync(email);

        if (biodata.IsError)
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
    [HttpGet("error")]
    public IActionResult Error(string? errorCode = "")
    {
        ViewData["ErrorCode"] = errorCode;
        return View(errorCode);
    }
}