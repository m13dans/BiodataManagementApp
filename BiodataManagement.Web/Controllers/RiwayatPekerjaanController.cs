using BiodataManagement.Domain.Entities;
using BiodataManagement.Extensions;
using BiodataManagement.Service.BiodataService;
using BiodataManagement.Web.Service.RiwayatPekerjaanService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiodataManagement.Web.Controllers;

[Authorize]
[Route("Biodata/{biodataId:int}/RiwayatPekerjaan")]
public class RiwayatPekerjaanController : Controller
{
    private readonly IRiwayatPekerjaanRepository _repo;
    private readonly IAuthorizationService _authorizationService;
    private readonly IBiodataRepository _bioRepo;
    public RiwayatPekerjaanController(IRiwayatPekerjaanRepository repo, IAuthorizationService authorizationService, IBiodataRepository bioRepo)
    {
        _repo = repo;
        _authorizationService = authorizationService;
        _bioRepo = bioRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFor([FromRoute] int biodataId)
    {
        var result = await _repo.GetAllRiwayatPekerjaanFor(biodataId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _repo.GetRiwayatPekerjaanById(id);

        if (result.IsError)
            return NotFound();

        return Ok(result.Value);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create([FromRoute] int biodataId)
    {
        var biodata = await _bioRepo.GetBiodataByIdAsync(biodataId);

        if (biodata.IsError)
            return NotFound();

        var validate = await _authorizationService.AuthorizeAsync(User, biodata.Value, "BiodataOwner");
        if (!validate.Succeeded)
            return Forbid();

        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        [FromRoute] int biodataId,
        RiwayatPekerjaanCreateRequest request,
        [FromServices] IValidator<RiwayatPekerjaanCreateRequest> validator)
    {
        var validate = await validator.ValidateAsync(request);
        if (!validate.IsValid)
        {
            validate.AddToModelState(ModelState);
            return View(request);
        }

        var result = await _repo.CreateRiwayatPekerjaan(biodataId, request);

        if (result.IsError)
            return BadRequest();

        return RedirectToAction("Detail", "Biodata", new { id = biodataId });
    }

    [HttpGet("Update/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var result = await _repo.GetRiwayatPekerjaanById(id);

        if (result.IsError)
            return NotFound();

        return View(result.Value);
    }

    [HttpPost("Update/{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        RiwayatPekerjaan riwayatPekerjaan,
        [FromServices] IValidator<RiwayatPekerjaan> validator)
    {
        if (!ModelState.IsValid)
            return View(riwayatPekerjaan);

        var validate = await validator.ValidateAsync(riwayatPekerjaan);
        if (!validate.IsValid)
        {
            validate.AddToModelState(ModelState);
            return View(riwayatPekerjaan);
        }
        var result = await _repo.UpdateRiwayatPekerjaan(id, riwayatPekerjaan);

        if (result.IsError)
            return BadRequest();

        return RedirectToAction("Detail", "Biodata", new { id = riwayatPekerjaan.BiodataId });
    }

    public async Task<IActionResult> Delete([FromRoute] int biodataId, int id)
    {
        var bio = await _bioRepo.GetBiodataByIdAsync(biodataId);
        if (bio.IsError)
            return NotFound();

        var validate = await _authorizationService.AuthorizeAsync(User, bio.Value, "BiodataOwner");
        if (!validate.Succeeded)
            return Forbid();

        var result = await _repo.DeleteRiwayatPekerjaanById(id);

        if (result.IsError)
            return BadRequest();

        return Ok(result.Value);
    }


}
