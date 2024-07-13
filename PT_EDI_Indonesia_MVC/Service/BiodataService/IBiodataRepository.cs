using PT_EDI_Indonesia_MVC.Domain.Entities;

namespace PT_EDI_Indonesia_MVC.Service.BiodataService;

public interface IBiodataRepository
{
    public Task<List<BiodataDTO>> GetBiodatasAsync();
    public Task<Biodata> GetBiodataAsync(int id);
    public Task<Biodata> GetBiodataWithEmailAsync(string email);
    public Task<List<PendidikanTerakhir>> GetPendidikansAsync(int biodataId);
    public Task<bool> UpdateBiodataAsync(Biodata biodata);
    public Task<bool> DeleteBiodataAsync(int id);
    public Task<bool> CreateBiodataAsync(Biodata biodata);
    public Task<int> GetCurrentUserId(string email);
}