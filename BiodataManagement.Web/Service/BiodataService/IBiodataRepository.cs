using ErrorOr;
using BiodataManagement.Domain.Entities;

namespace BiodataManagement.Service.BiodataService;

public interface IBiodataRepository
{
    public Task<ErrorOr<List<BiodataDTO>>> GetBiodataListAsync();
    public Task<ErrorOr<Biodata>> GetBiodataByIdAsync(int id);
    public Task<ErrorOr<Biodata>> GetBiodataWithEmailAsync(string email);
    public Task<List<PendidikanTerakhir>> GetPendidikansAsync(int biodataId);
    public Task<bool> UpdateBiodataAsync(int biodataId, BiodataUpdateRequest biodata);
    public Task<bool> UpdateBiodataByAdminAsync(int biodataId, BiodataUpdateRequest biodata);
    public Task<bool> DeleteBiodataAsync(int id);
    public Task<bool> CreateListBiodataAsync(Biodata biodata);
    public Task<int> GetCurrentUserId(string email);

    public Task<string> GetBiodataOwnerId(string userId);
    public Task<ErrorOr<Biodata>> GetBiodataWithUserId(string userId);
    public Task<bool> ValidateBiodataOwner(string userEmail);
    public Task<ErrorOr<AppUserBiodata>> GetAppUserBiodataAsync(string userId);
    public Task<bool> IsBiodataExist(string userId, string userEmail);
    public Task<bool> IsKTPExists(string ktp);
    public Task<ErrorOr<Biodata>> CreateBiodataAsync(BiodataCreateRequest biodata);


}