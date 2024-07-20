using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Permissions;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiodataManagement.Data.Repository;
using BiodataManagement.Data.Seed;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Service.Accounts;
using BiodataManagement.Service.BiodataService;

namespace BiodataManagement.Controllers;

[Authorize]
[Route("Biodata")]
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
    public async Task<JsonResult> GenerateData(GenerateData generateData)
    {
        var result = await generateData.SubmitBiodata();
        return Json(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Displaydata")]
    public async Task<IActionResult> DisplayData()
    {
        var result = await _bioRepo.GetBiodataListAsync();

        return PartialView("_BiodataListPartial", result.Value);
    }

    [HttpGet("Detail")]
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

    [HttpGet("Detail/{id:int}")]
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


    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var biodata = await _bioRepo.GetBiodataWithUserId(userId);

        if (!biodata.IsError)
            return RedirectToAction("Update", new { id = biodata.Value.Id });

        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(Biodata biodata)
    {
        if (!ModelState.IsValid)
            return View(biodata);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var biodataExist = await _bioRepo.IsBiodataExist(userId, userEmail);

        if (biodataExist)
            return View("Biodata.BadRequest", "Biodata.AlreadyExist");

        biodata.UserId = userId;
        await _bioRepo.CreateBiodataAsync(biodata);
        return RedirectToAction("Detail", "Biodata", new { id = biodata.Id });
    }

    [HttpGet("Update")]
    public async Task<IActionResult> Update()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var biodata = await _bioRepo.GetBiodataWithUserId(userId);

        var result = biodata.MatchFirst(
            onValue: View,
            onFirstError: error => View("Biodata.NotFound")
        );

        return result;
    }

    [HttpGet("Update/{id:int}")]
    public async Task<IActionResult> Update(int id)
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
    [HttpPost("Update/{id:int}")]
    public async Task<IActionResult> Update(int id, Biodata biodata)
    {
        if (!ModelState.IsValid)
            return View(biodata);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        biodata.UserId = userId;

        var result = await _bioRepo.UpdateBiodataAsync(biodata);
        if (result is false)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return View("Biodata.BadRequest");
        }
        return RedirectToAction("Detail");
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _bioRepo.DeleteBiodataAsync(id);
        if (result is false)
            return NotFound();

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