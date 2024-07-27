using BiodataManagement.Service.BiodataService;
using BiodataManagement.Web.Service.PendidikanTerakhirService;
using FluentValidation;

namespace BiodataManagement.Service.PendidikanTerakhirService;

public class PendidikanTerakhirValidator : AbstractValidator<PendidikanTerakhirRequest>
{
    private readonly PendidikanTerakhirService _service;
    public PendidikanTerakhirValidator(PendidikanTerakhirService service)
    {
        _service = service;

        RuleFor(x => x.BiodataId).MustAsync(async (bioId, ct) => await _service.IsBiodataExist(bioId))
            .WithMessage(x => $"Biodata with id {x.BiodataId} doesn't exists try to create biodata first.");
    }
}

public class PendidikanTerakhirService
{
    private readonly IPendidikanTerakhirRepository _repo;
    public PendidikanTerakhirService(IPendidikanTerakhirRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> IsBiodataExist(int biodataId)
    {
        var isExist = await _repo.GetAllPendidikanTerakhirForAsync(biodataId);
        if (isExist.IsError)
            return false;

        return true;
    }
}
