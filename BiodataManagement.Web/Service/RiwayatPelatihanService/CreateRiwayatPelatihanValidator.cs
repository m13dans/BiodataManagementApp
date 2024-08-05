using ErrorOr;
using FluentValidation;

namespace BiodataManagement.Web.Service.RiwayatPelatihanService;

public class CreateRiwayatPelatihanValidator : AbstractValidator<RiwayatPelatihanCreateRequest>
{
    private readonly IRiwayatPelatihanRepository _repo;
    public CreateRiwayatPelatihanValidator(IRiwayatPelatihanRepository repo)
    {
        _repo = repo;

        RuleFor(x => x).MustAsync(async (x, ct) =>
        {
            var riwayatPelatihan = await _repo.GetAllFor(x.BiodataId);
            return riwayatPelatihan.FirstError.Type == ErrorType.NotFound ||
                riwayatPelatihan.Value.Count < 3;
        }).WithName("Riwayat Pelatihan").WithMessage("Cannot create more than 3 riwayat pekerjaan");

        RuleFor(x => x.NamaKursus).NotEmpty();
        RuleFor(x => x.Tahun).NotEmpty().GreaterThan(2000);
    }
}
