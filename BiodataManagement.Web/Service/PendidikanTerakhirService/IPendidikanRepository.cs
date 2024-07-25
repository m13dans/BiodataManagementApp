using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Service.PendidikanTerakhirService;
using ErrorOr;

namespace BiodataManagement.Web.Service.PendidikanTerakhirService;

public interface IPendidikanTerakhirRepository
{
    public Task<ErrorOr<List<PendidikanTerakhir>>> GetAllPendidikanTerakhirForAsync(int biodataId);
    public Task<ErrorOr<PendidikanTerakhir>> GetPendidikanTerakhirByIdAsync(int id);
    public Task<ErrorOr<PendidikanTerakhir>> UpdataPendidikanTerakhirByIdAsync(int id, PendidikanTerakhirRequest request);
    public Task<ErrorOr<PendidikanTerakhir>> CreatePendidikanTerakhirAsync(int biodataId, PendidikanTerakhirRequest request);
    public Task<ErrorOr<List<PendidikanTerakhir>>> CreateListPendidikanTerakhirAsync(int biodataId, IEnumerable<PendidikanTerakhirRequest> request);
    public Task<ErrorOr<bool>> DeletePendidikanTerakhirByIdAsync(int id);
}
