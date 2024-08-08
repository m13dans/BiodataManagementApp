using BiodataManagement.Domain.Entities;
using BiodataManagement.Extensions;
using BiodataManagement.Web.Service.RiwayatPelatihanService;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiodataManagement.Web.Controllers;

[Authorize]
[Route("Biodata/{biodataId:int}/RiwayatPelatihan")]
public class RiwayatPelatihanController : Controller
{
    private readonly IRiwayatPelatihanRepository _repo;
    private readonly IAuthorizationService _authorizationService;
    public RiwayatPelatihanController(IRiwayatPelatihanRepository repo, IAuthorizationService authorizationService)
    {
        _repo = repo;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFor([FromRoute] int biodataId)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        var result = await _repo.GetAllFor(biodataId);

        return result.MatchFirst<ActionResult>(
            onValue: View,
            onFirstError: error =>
                error.Type is ErrorType.NotFound ? View() : BadRequest());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int biodataId, int id)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        var result = await _repo.GetById(id);

        return result.MatchFirst<ActionResult>(
            onValue: View,
            onFirstError: error =>
                error.Type is ErrorType.NotFound ? NotFound() : BadRequest());
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(
        [FromRoute] int biodataId,
        RiwayatPelatihanCreateRequest request)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        [FromRoute] int biodataId,
        [FromServices] IValidator<RiwayatPelatihanCreateRequest> validator,
        RiwayatPelatihanCreateRequest request)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        var validate = await validator.ValidateAsync(request);
        if (!validate.IsValid)
        {
            validate.AddToModelState(ModelState);
            return View(request);
        }

        var result = await _repo.CreateFor(biodataId, request);

        if (result.IsError)
            return BadRequest();

        return RedirectToAction("Detail", "Biodata", new { id = biodataId });
    }

    [HttpGet("Update/{id:int}")]
    public async Task<IActionResult> Update(
       [FromRoute] int biodataId, int id)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        var result = await _repo.GetById(id);

        return result.MatchFirst<ActionResult>(
            onValue: View,
            onFirstError: error => NotFound());
    }

    [HttpPost("Update/{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int biodataId,
        int id,
        [FromServices] IValidator<RiwayatPelatihan> validator,
        RiwayatPelatihan riwayatPelatihan)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        var validate = await validator.ValidateAsync(riwayatPelatihan);
        if (!validate.IsValid)
        {
            validate.AddToModelState(ModelState);
            return View(riwayatPelatihan);
        }

        var result = await _repo.Update(id, riwayatPelatihan);

        if (result.IsError)
            return BadRequest();

        return RedirectToAction("Detail", "Biodata", new { id = biodataId });
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int biodataId, int id)
    {
        var auth = await _authorizationService.AuthorizeAsync(User, biodataId, "CanEditBiodata");
        if (!auth.Succeeded)
            return Forbid();

        var result = await _repo.Delete(id);

        return result.MatchFirst<ActionResult>(
            onValue: Ok,
            onFirstError: error => BadRequest());
    }

}
