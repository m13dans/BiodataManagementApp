using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiodataManagement.Data.Seed;
using BiodataManagement.Service.BiodataService;
using FluentValidation;
using BiodataManagement.Extensions;
using BiodataManagement.Domain.Entities;

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
    public async Task<JsonResult> GenerateData([FromServices] GenerateData generateData)
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

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteAllFakeData")]
    public async Task<IActionResult> DeleteAllFakeData([FromServices] GenerateData generateData)
    {
        int result = await generateData.DeleteAllFakeBiodata();

        return Json(result);
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
            return View("Biodata.NotFound", id);

        var authorizeResult = await _authorizeService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizeResult.Succeeded)
            return View("Biodata.Forbidden");

        return View(biodata.Value);
    }


    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var isBiodataExist = await _bioRepo.IsBiodataExist(userId, userEmail);
        if (!isBiodataExist)
            return View();

        var biodataByUserId = await _bioRepo.GetBiodataWithUserId(userId);

        if (!biodataByUserId.IsError)
            return RedirectToAction("Update", new { id = biodataByUserId.Value.Id });

        var biodataByEmail = await _bioRepo.GetBiodataWithEmailAsync(userEmail);
        return RedirectToAction("Update", new { id = biodataByEmail.Value.Id });

    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromServices] IValidator<BiodataCreateRequest> validator, BiodataCreateRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        request.UserId = userId;

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(request);
        }

        var biodata = await _bioRepo.CreateBiodataAsync(request);
        if (biodata.IsError)
        {
            ModelState.TryAddModelError(biodata.FirstError.Code, "Cannot Create Biodata");
            return View();
        }

        return RedirectToAction("Detail", new { id = biodata.Value.Id });
    }

    [HttpGet("Update")]
    public async Task<IActionResult> Update()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var biodata = await _bioRepo.GetBiodataWithUserId(userId);

        var result = biodata.MatchFirst<ActionResult>(
            onValue: value => RedirectToAction("Update", new { id = biodata.Value.Id }),
            onFirstError: error => View("Biodata.NotFound"));

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
    public async Task<IActionResult> Update(
        [FromServices] IValidator<Biodata> validator,
        int biodataId,
        Biodata biodataRequest)
    {
        var authorizeResult = await _authorizeService.AuthorizeAsync(User, biodataRequest, "BiodataOwner");
        if (!authorizeResult.Succeeded)
            return Forbid();

        var validationResult = await validator.ValidateAsync(biodataRequest);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(biodataRequest);
        }

        var result = await _bioRepo.UpdateBiodataAsync(biodataId, biodataRequest);

        return RedirectToAction("Detail", new { id = biodataId });
    }


    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var biodata = await _bioRepo.GetBiodataByIdAsync(id);
        if (biodata.IsError)
            return View("Biodata.NotFound", id);

        var authorizeResult = await _authorizeService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizeResult.Succeeded)
            return Forbid();

        var result = await _bioRepo.DeleteBiodataAsync(id);
        if (result is false)
            return BadRequest();

        return RedirectToAction("Index", "Home");
    }
}