using BiodataManagement.Domain.Entities;
using FluentValidation;

namespace BiodataManagement.Web.Service.RiwayatPelatihanService;

public class UpdateRiwayatPelatihanValidator : AbstractValidator<RiwayatPelatihan>
{
    public UpdateRiwayatPelatihanValidator()
    {
        RuleFor(x => x.NamaKursus).NotEmpty();
        RuleFor(x => x.Tahun).NotEmpty().GreaterThan(2000);
    }
}
