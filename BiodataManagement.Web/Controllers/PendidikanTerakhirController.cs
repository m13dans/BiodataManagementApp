using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiodataManagement.Service.BiodataService;
using BiodataManagement.Service.PendidikanTerakhirService;
using BiodataManagement.Web.Service.PendidikanTerakhirService;
using FluentValidation;
using BiodataManagement.Extensions;
using BiodataManagement.Domain.Entities;

namespace BiodataManagement.Controllers;

[Authorize]
[Route("Biodata/{biodataId:int}/PendidikanTerakhir")]
public class PendidikanTerakhirController : Controller
{
    private readonly ILogger<PendidikanTerakhirController> _logger;
    private readonly IPendidikanTerakhirRepository _pendidikanRepo;
    private readonly IBiodataRepository _biodataRepository;
    private readonly IAuthorizationService _autthorizationService;


    public PendidikanTerakhirController(ILogger<PendidikanTerakhirController> logger,
        IPendidikanTerakhirRepository pendidikanRepo,
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
    public async Task<IActionResult> Index([FromRoute] int biodataId)
    {
        var biodata = await _biodataRepository.GetBiodataByIdAsync(biodataId);

        if (biodata.IsError)
            return View("Biodata.NotFound");

        var authResult = await _autthorizationService.AuthorizeAsync(User, biodata, "BiodataOwner");
        if (!authResult.Succeeded)
            return Forbid();

        var pendidikanList = await _pendidikanRepo.GetAllPendidikanTerakhirForAsync(biodataId);

        if (pendidikanList.IsError)
            return View();

        return View(pendidikanList.Value);
    }

    [HttpGet("Create")]

    public async Task<IActionResult> Create([FromRoute] int biodataId)
    {
        var biodata = await _biodataRepository.GetBiodataByIdAsync(biodataId);

        var authResult = await _autthorizationService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");

        if (!authResult.Succeeded)
            return Forbid();

        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromServices] IValidator<PendidikanTerakhirRequest> validator,
        [FromRoute] int biodataId,
        PendidikanTerakhirRequest request)
    {
        var biodata = await _biodataRepository.GetBiodataByIdAsync(biodataId);
        var authorizationResult = await _autthorizationService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!authorizationResult.Succeeded)
            return Forbid();

        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            validatorResult.AddToModelState(ModelState);
            return View(request);
        }

        var result = await _pendidikanRepo.CreatePendidikanTerakhirAsync(biodataId, request);

        if (result.IsError)
            return BadRequest();

        return RedirectToAction("Detail", "Biodata", new { id = biodataId });
    }

    [HttpGet("Update/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int biodataId, int id)
    {
        var bio = await _biodataRepository.GetBiodataByIdAsync(biodataId);
        if (bio.IsError)
            return NotFound();

        var authorize = await _autthorizationService.AuthorizeAsync(User, bio.Value, "BiodataOwner");
        if (!authorize.Succeeded)
            return Forbid();

        var pendidikan = await _pendidikanRepo.GetPendidikanTerakhirByIdAsync(id);
        if (pendidikan.IsError)
            return NotFound();

        return View(pendidikan.Value);
    }

    [HttpPost("Update/{id:int}")]
    public async Task<IActionResult> Update(
        [FromServices] IValidator<PendidikanTerakhir> validator,
        [FromRoute] int biodataId,
        int id,
        PendidikanTerakhir pendidikan)
    {
        var validate = await validator.ValidateAsync(pendidikan);
        if (!validate.IsValid)
        {
            validate.AddToModelState(ModelState);
            return View(pendidikan);
        }
        var result = await _pendidikanRepo.UpdataPendidikanTerakhirByIdAsync(id, pendidikan);
        if (result.IsError)
        {
            return BadRequest();
        }
        return RedirectToAction("Detail", "Biodata", new { id = biodataId });
    }


    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int biodataId, int id)
    {
        var bio = await _biodataRepository.GetBiodataByIdAsync(biodataId);
        if (bio.IsError)
            return NotFound();

        var authorize = await _autthorizationService.AuthorizeAsync(User, bio.Value, "BiodataOwner");
        if (!authorize.Succeeded)
            return Forbid();

        var result = await _pendidikanRepo.DeletePendidikanTerakhirByIdAsync(id);
        if (result.IsError)
            return NotFound();

        return RedirectToAction("Detail", "Biodata", new { id = biodataId });
    }


    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View("Error!");
    // }
}