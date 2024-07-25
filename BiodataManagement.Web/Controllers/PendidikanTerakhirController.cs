using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiodataManagement.Data.Repository;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Service.BiodataService;
using BiodataManagement.Service.PendidikanTerakhirService;

namespace BiodataManagement.Controllers;

[Authorize]
[Route("{BiodataId:int}/PendidikanTerakhir")]
public class PendidikanTerakhirController : Controller
{
    private readonly ILogger<PendidikanTerakhirController> _logger;
    private readonly PendidikanTerakhirRepository _pendidikanRepo;
    private readonly IBiodataRepository _biodataRepository;
    private readonly IAuthorizationService _autthorizationService;


    public PendidikanTerakhirController(ILogger<PendidikanTerakhirController> logger,
        PendidikanTerakhirRepository pendidikanRepo,
        IBiodataRepository biodataRepository,
        IAuthorizationService authorizationService
    )
    {
        _biodataRepository = biodataRepository;
        _pendidikanRepo = pendidikanRepo;
        _logger = logger;
        _autthorizationService = authorizationService;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var pendidikanList = await _pendidikanRepo.GetPendidikanByUserIdAsync(userId);

        if (pendidikanList.IsError)
            return View();

        return View(pendidikanList.Value);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _pendidikanRepo.GetPendidikanByUserIdAsync(userId);

        // if result is null or result.Value.Count is 0 
        if (result.IsError)
            return View();

        var pendidikanList = result.Value;
        if (pendidikanList.Count >= 3)
            return RedirectToAction("update");

        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(int biodataId, PendidikanTerakhirRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var biodata = await _biodataRepository.GetBiodataWithUserId(userId);

        if (biodata.IsError)
        {
            ModelState.TryAddModelError("", "You have to create biodata first");
            return View(request);
        }

        var authorizationResult = await _autthorizationService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizationResult.Succeeded)
            return Forbid();

        var result = await _pendidikanRepo.CreatePendidikanTerakhirAsync(biodataId, request);

        if (result.IsError)
            return BadRequest();

        return RedirectToAction("Detail", "Biodata");
    }

    [HttpGet("Update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var isUserHaveBiodata = await _biodataRepository.IsBiodataExist(userId, userEmail);
        if (!isUserHaveBiodata)
            return View("Biodata.NotFound");

        var biodata = await _biodataRepository.GetBiodataWithUserId(userId);
        if (biodata.IsError)
        {
            var biodataByEmail = await _biodataRepository.GetBiodataWithEmailAsync(userEmail);
            var authorizationResult2 = await _autthorizationService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
            if (!authorizationResult2.Succeeded)
                return Forbid();

            return View(biodataByEmail.Value);
        }


        var authorizationResult = await _autthorizationService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizationResult.Succeeded)
            return Forbid();


        return View("Biodata.NotFound");
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


    public async Task<IActionResult> Delete(int id)
    {
        var result = await _pendidikanRepo.DeletePendidikanTerakhirByIdAsync(id);
        if (result.IsError)
            return NotFound();

        return RedirectToAction("Detail", "Biodata");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}