using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Permissions;
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
    private readonly IAuthorizationService _authorizeService;
    private readonly IBiodataRepository _bioRepo;


    public BiodataController(
        ILogger<BiodataController> logger,
        IBiodataRepository bioRepo,
        IAuthorizationService authorizationService)
    {
        _authorizeService = authorizationService;
        _bioRepo = bioRepo;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        ErrorOr<List<BiodataDTO>> errorOrResult = await _bioRepo.GetBiodataListAsync();

        return errorOrResult.MatchFirst(
            onValue: View,
            onFirstError: error =>
            {
                _logger.LogWarning($"{error.Code} {error.Description}");
                return View();
            });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("GenerateData")]
    public async Task<JsonResult> GenerateData([FromServices] GenerateData generateData)
    {
        var result = await generateData.SubmitBiodata();
        return Json(result);
    }

    [HttpGet("displaydata")]
    public async Task<IActionResult> DisplayData()
    {
        var result = await _bioRepo.GetBiodataListAsync();

        return PartialView("_BiodataListPartial", result.Value);
    }

    [HttpGet("detail")]
    public async Task<IActionResult> Detail()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var biodata = await _bioRepo.GetBiodataWithUserId(userId);

        var result = biodata.MatchFirst(
            onValue: View,
            onFirstError: error => View("Biodata.NotFound")
        );

        return result;
    }

    [Authorize]
    [HttpGet("detail/{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var biodata = await _bioRepo.GetBiodataByIdAsync(id);

        if (biodata.IsError)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return View("Biodata.NotFound", id);
        }

        var authorizeResult = await _authorizeService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizeResult.Succeeded)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            return View("Biodata.Forbidden");
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
    [HttpPost("create")]
    public async Task<IActionResult> Create(Biodata biodata)
    {
        if (!ModelState.IsValid)
            return View(biodata);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        biodata.UserId = userId;

        var biodataExist = await _bioRepo.IsBiodataExist(userId);

        if (biodataExist)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return View("Biodata.BadRequest");
        }

        await _bioRepo.CreateBiodataAsync(biodata);
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
    [HttpPost("update")]
    public async Task<IActionResult> Update(Biodata biodata)
    {
        if (!ModelState.IsValid)
        {
            return View(biodata);
        }

        var result = await _bioRepo.UpdateBiodataAsync(biodata);
        if (result is false)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return View("Biodata.BadRequest");
        }
        return RedirectToAction("Index", "Home");
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _bioRepo.DeleteBiodataAsync(id);
        if (result is false)
        {
            return NotFound();
        }
        return RedirectToAction("Index", "Home");
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // [HttpGet("error")]
    // public IActionResult Error(string? errorCode = "")
    // {
    //     ViewData["ErrorCode"] = errorCode;
    //     return View(errorCode);
    // }
}