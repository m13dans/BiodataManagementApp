using FluentValidation;

namespace BiodataManagement.Web.Service.RiwayatPekerjaanService;

public class CreateRiwayatPekerjaanValidator : AbstractValidator<RiwayatPekerjaanCreateRequest>
{
    private readonly IRiwayatPekerjaanRepository _repo;
    public CreateRiwayatPekerjaanValidator(IRiwayatPekerjaanRepository repo)
    {
        _repo = repo;

        RuleFor(x => x).MustAsync(async (x, ct) =>
        {
            var riwayatPekerjaan = await _repo.GetAllRiwayatPekerjaanFor(x.BiodataId);
            return riwayatPekerjaan.Count < 3;
        }).WithName("RiwayatPekerjaan").WithMessage("Cannot create more than 3 riwayat pekerjaan");

        RuleFor(x => x.NamaPerusahaan).NotEmpty();
        RuleFor(x => x.PosisiTerakhir).NotEmpty();
        RuleFor(x => x.PendapatanTerakhir).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Tahun).NotEmpty().GreaterThan(0);
    }
}
