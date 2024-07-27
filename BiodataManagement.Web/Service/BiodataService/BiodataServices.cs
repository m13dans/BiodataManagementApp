namespace BiodataManagement.Service.BiodataService;

public class BiodataServices
{
    private readonly IBiodataRepository _repo;
    public BiodataServices(IBiodataRepository repo)
    {
        _repo = repo;
    }

}
