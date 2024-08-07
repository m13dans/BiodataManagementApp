using ErrorOr;
using BiodataManagement.Domain.Entities;

namespace BiodataManagement.Service.BiodataService;

public interface IBiodataRepository
{
    public Task<ErrorOr<Biodata>> CreateBiodataAsync(BiodataCreateRequest biodata);
    public Task<ErrorOr<List<BiodataDTO>>> GetBiodataListAsync();
    public Task<ErrorOr<List<BiodataDTO>>> GetBiodataListAsync(string nama, string posisiDilamar);
    public Task<ErrorOr<Biodata>> GetBiodataByIdAsync(int id);
    public Task<ErrorOr<Biodata>> GetBiodataWithEmailAsync(string email);
    public Task<bool> UpdateBiodataAsync(int biodataId, Biodata biodata);
    public Task<bool> DeleteBiodataAsync(int id);
    public Task<bool> CreateListBiodataAsync(Biodata biodata);
    public Task<ErrorOr<Biodata>> GetBiodataWithUserId(string userId);
    public Task<bool> IsBiodataExist(string userId, string userEmail);
    public Task<bool> IsKTPExists(string ktp);
    public Task<bool> CanChangeEmail(int biodataId, string email);
}