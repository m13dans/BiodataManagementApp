using BiodataManagement.Domain.Entities;
using ErrorOr;

namespace BiodataManagement.Web.Service.RiwayatPelatihanService;

public interface IRiwayatPelatihanRepository
{
    public Task<ErrorOr<RiwayatPelatihan>> GetById(int id);
    public Task<ErrorOr<RiwayatPelatihan>> CreateFor(int biodataId, RiwayatPelatihanCreateRequest request);
    public Task<ErrorOr<RiwayatPelatihan>> Update(int id, RiwayatPelatihan riwayatPelatihan);
    public Task<ErrorOr<RiwayatPelatihan>> Delete(int id);
    public Task<ErrorOr<List<RiwayatPelatihan>>> GetAllFor(int biodataId);
}
