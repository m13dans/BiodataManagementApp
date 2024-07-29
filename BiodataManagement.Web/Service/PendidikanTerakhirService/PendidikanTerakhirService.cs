using BiodataManagement.Web.Service.PendidikanTerakhirService;

namespace BiodataManagement.Service.PendidikanTerakhirService;

public class PendidikanTerakhirService
{
    private readonly IPendidikanTerakhirRepository _repo;
    public PendidikanTerakhirService(IPendidikanTerakhirRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> IsBiodataExist(int biodataId) =>
         await _repo.IsBiodataExist(biodataId);

    public async Task<bool> PendidikanIsLessThanThree(int biodataId)
    {
        var maxPendidikanListCount = 3;

        var pendidikanList = await _repo.GetAllPendidikanTerakhirForAsync(biodataId);
        bool lessThanThree = pendidikanList.MatchFirst(
            x => x.Count < maxPendidikanListCount,
            _ => true
        );

        return lessThanThree;
    }
}
