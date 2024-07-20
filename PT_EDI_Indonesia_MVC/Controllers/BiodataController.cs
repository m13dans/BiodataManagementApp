using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiodataManagement.Data.Seed;
using BiodataManagement.Domain.Entities;
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
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var isBiodataExist = await _bioRepo.IsBiodataExist(userId, userEmail);
        if (!isBiodataExist)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return View("Biodata.NotFound");
        }

        var biodataWithUserId = await _bioRepo.GetBiodataWithUserId(userId);
        if (!biodataWithUserId.IsError)
            return View(biodataWithUserId.Value);

        var biodataWithEmail = await _bioRepo.GetBiodataWithEmailAsync(userEmail);

        return View(biodataWithEmail.Value);
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
        return RedirectToAction("Detail", new { id = biodata.Id });
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

    [HttpPost("Update/{BiodataId:int}")]
    public async Task<IActionResult> Update(int biodataId, Biodata biodataRequest)
    {
        if (!ModelState.IsValid)
            return View(biodataRequest);

        var biodata = await _bioRepo.GetBiodataByIdAsync(biodataId);

        if (biodata.IsError)
            return View("Biodata.NotFound", new { biodataId });

        var authorizeResult = await _authorizeService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizeResult.Succeeded)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            return View("Biodata.Forbidden");
        }

        if (User.IsInRole("Admin"))
        {
            var updateResult = await _bioRepo.UpdateBiodataByAdminAsync(biodataId, biodataRequest);
            if (updateResult is false)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return View("Biodata.BadRequest");
            }

            return RedirectToAction("Biodata");
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        biodataRequest.UserId = userId;

        var result = await _bioRepo.UpdateBiodataAsync(biodataId, biodataRequest);
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