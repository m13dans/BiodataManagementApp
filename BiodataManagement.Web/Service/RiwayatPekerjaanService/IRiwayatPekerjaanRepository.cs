using BiodataManagement.Domain.Entities;
using ErrorOr;

namespace BiodataManagement.Web.Service.RiwayatPekerjaanService;

public interface IRiwayatPekerjaanRepository
{
    public Task<List<RiwayatPekerjaan>> GetAllRiwayatPekerjaanFor(int biodataId);
    public Task<ErrorOr<RiwayatPekerjaan>> GetRiwayatPekerjaanById(int id);
    public Task<ErrorOr<RiwayatPekerjaan>> CreateRiwayatPekerjaan(int biodataId, RiwayatPekerjaanCreateRequest request);
    public Task<ErrorOr<RiwayatPekerjaan>> UpdateRiwayatPekerjaan(int id, RiwayatPekerjaan riwayatPekerjaan);
    public Task<ErrorOr<RiwayatPekerjaan>> DeleteRiwayatPekerjaanById(int id);
}
