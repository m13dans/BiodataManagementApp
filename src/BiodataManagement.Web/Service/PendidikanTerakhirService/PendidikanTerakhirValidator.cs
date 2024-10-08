using BiodataManagement.Domain.Entities;
using FluentValidation;

namespace BiodataManagement.Service.PendidikanTerakhirService;

public class PendidikanTerakhirCreateValidator : AbstractValidator<PendidikanTerakhirRequest>
{
    private readonly PendidikanTerakhirService _service;
    public PendidikanTerakhirCreateValidator(PendidikanTerakhirService service)
    {
        _service = service;

        RuleFor(x => x.BiodataId).MustAsync(async (bioId, ct) => await _service.IsBiodataExist(bioId))
            .WithMessage(x => $"Biodata with id {x.BiodataId} doesn't exists try to create biodata first.");

        RuleFor(x => x)
            .MustAsync(async (req, ct) => await _service.PendidikanIsLessThanThree(req.BiodataId))
            .WithMessage($"Cannot create more than three pendidikan Terakhir List");

        RuleFor(x => x.IPK).NotEmpty().InclusiveBetween(0.1f, 4.0f);
    }
}

public class PendidikanTerakhirUpdateValidator : AbstractValidator<PendidikanTerakhir>
{
    private readonly PendidikanTerakhirService _service;
    public PendidikanTerakhirUpdateValidator(PendidikanTerakhirService service)
    {
        _service = service;

        RuleFor(x => x.BiodataId).MustAsync(async (bioId, ct) => await _service.IsBiodataExist(bioId))
            .WithMessage(x => $"Biodata with id {x.BiodataId} doesn't exists try to create biodata first.");

        RuleFor(x => x.IPK).NotEmpty().InclusiveBetween(0.1f, 4.0f);
    }
}
