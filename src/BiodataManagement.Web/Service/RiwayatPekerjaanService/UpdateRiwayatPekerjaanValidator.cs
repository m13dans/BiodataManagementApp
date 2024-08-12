using BiodataManagement.Domain.Entities;
using FluentValidation;

namespace BiodataManagement.Web.Service.RiwayatPekerjaanService;

public class UpdateRiwayatPekerjaanValidator : AbstractValidator<RiwayatPekerjaan>
{
    public UpdateRiwayatPekerjaanValidator()
    {
        RuleFor(x => x.NamaPerusahaan).NotEmpty();
        RuleFor(x => x.PosisiTerakhir).NotEmpty();
        RuleFor(x => x.PendapatanTerakhir).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Tahun).NotEmpty().GreaterThan(0);
    }
}
