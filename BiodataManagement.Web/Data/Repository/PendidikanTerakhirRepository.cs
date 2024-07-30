using System.Data;
using Dapper;
using BiodataManagement.Data.Context;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Web.Service.PendidikanTerakhirService;
using ErrorOr;
using BiodataManagement.Service.PendidikanTerakhirService;

namespace BiodataManagement.Data.Repository;

public class PendidikanTerakhirRepository : IPendidikanTerakhirRepository
{
    private readonly DbConnectionFactory _context;
    public PendidikanTerakhirRepository(DbConnectionFactory context)
    {
        _context = context;

    }
    public async Task<ErrorOr<List<PendidikanTerakhir>>> GetAllPendidikanTerakhirForAsync(int biodataId)
    {
        var query = "usp_PendidikanTerakhir_GetAllFor";
        using var connection = _context.CreateConnection();

        var listPendidikan = await connection.QueryAsync<PendidikanTerakhir>(
            query,
            new { biodataId },
            commandType: CommandType.StoredProcedure);

        if (listPendidikan is null || listPendidikan.Count() is 0)
            return Error.NotFound("PendidikanTerakhir.NotFound");

        var result = listPendidikan.ToList();
        return result;
    }

    public async Task<ErrorOr<List<PendidikanTerakhir>>> GetPendidikanByUserIdAsync(string userId)
    {
        var query = @"SELECT * FROM PendidikanTerakhir p 
            JOIN Biodata ON b.Id = p.BiodataId
            WHERE b.UserId = @UserId";
        using var connection = _context.CreateConnection();

        var pendidikans = await connection.QueryAsync<PendidikanTerakhir>(
            query,
            new { UserId = userId },
            commandType: CommandType.StoredProcedure);

        if (pendidikans is null || pendidikans.Count() < 1)
            return Error.NotFound("PendidikanTerakhir.NotFound");

        return pendidikans.ToList();
    }

    public async Task<bool> UpdatePendidikanAsync(PendidikanTerakhir pendidikans)
    {
        var query = "usp_PendidikanTerakhir_Update";
        using var connection = _context.CreateConnection();

        var pendidikan = await connection.ExecuteAsync(
            query,
            new
            {
                id = pendidikans.Id,
                jenjangPendidikanTerakhir = pendidikans.JenjangPendidikanTerakhir,
                namaInstitusiAkademik = pendidikans.NamaInstitusiAkademik,
                jurusan = pendidikans.Jurusan,
                tahunLulus = pendidikans.TahunLulus,
                ipk = pendidikans.IPK
            },
            commandType: CommandType.StoredProcedure);

        return pendidikan > 0;

    }

    // Without Stored Procedure

    public async Task<ErrorOr<PendidikanTerakhir>> GetPendidikanTerakhirByIdAsync(int id)
    {
        var query = "SELECT * FROM PendidikanTerakhir WHERE Id = @Id";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<PendidikanTerakhir>(
            query,
            new { Id = id });

        if (result is null)
            return Error.NotFound("PendidikanTerakhir.NotFound");

        return result;
    }

    public async Task<ErrorOr<PendidikanTerakhir>> UpdataPendidikanTerakhirByIdAsync(int id, PendidikanTerakhir request)
    {
        var updateCommand =
            @"UPDATE PendidikanTerakhir SET
                BiodataId = @BiodataId,
                JenjangPendidikanTerakhir = @JenjangPendidikanTerakhir,
                NamaInstitusiAkademik = @NamaInstitusiAkademik,
                Jurusan = @Jurusan,
                TahunLulus = @TahunLulus,
                IPK = @IPK
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var updateResult = await connection.ExecuteAsync(
            updateCommand,
            new
            {
                request.BiodataId,
                request.JenjangPendidikanTerakhir,
                request.NamaInstitusiAkademik,
                request.Jurusan,
                request.TahunLulus,
                request.IPK,
                Id = id
            });

        if (updateResult is 0)
            return Error.Failure("PendidikanTerakhir.UpdateFailure");

        var query = "SELECT * FROM PendidikanTerakhir WHERE Id = @Id";
        PendidikanTerakhir result = await connection.QuerySingleAsync<PendidikanTerakhir>(
            query,
            new { Id = id }
        );

        return result;
    }

    public async Task<ErrorOr<PendidikanTerakhir>> CreatePendidikanTerakhirAsync(int biodataId, PendidikanTerakhirRequest request)
    {
        var createCommand = "usp_PendidikanTerakhir_Insert";

        using var connection = _context.CreateConnection();
        var createResult = await connection.QueryFirstOrDefaultAsync<PendidikanTerakhir>(
            createCommand,
            new
            {
                request.BiodataId,
                request.JenjangPendidikanTerakhir,
                request.NamaInstitusiAkademik,
                request.Jurusan,
                request.TahunLulus,
                Ipk = request.IPK,
            },
            commandType: CommandType.StoredProcedure);

        if (createResult is null)
            return Error.Failure("PendidikanTerakhir.CreateFailure");

        return createResult;
    }

    public async Task<ErrorOr<List<PendidikanTerakhir>>> CreateListPendidikanTerakhirAsync(
        int biodataId,
        IEnumerable<PendidikanTerakhirRequest> requests)
    {
        if (requests is null)
            return Error.NotFound();

        DataTable pendidikanTable = TableHelper.CreatePendidikanTerakhirTable(requests);
        var tvp = pendidikanTable.AsTableValuedParameter("UDT_PendidikanTerakhir");

        var query = "usp_PendidikanTerakhir_Create";

        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<PendidikanTerakhir>(
            query,
            new { UdtPendidikanTerakhir = tvp },
            commandType: CommandType.StoredProcedure
        );

        if (result is null)
            return Error.Failure("PendidikanTerakhir.Failure");

        return result.ToList();
    }

    public async Task<ErrorOr<bool>> DeletePendidikanTerakhirByIdAsync(int id)
    {
        var query = "DELETE FROM PendidikanTerakhir WHERE Id = @Id";
        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteAsync(
            query,
            new { Id = id });

        return result switch
        {
            < 1 => Error.Failure("PendidikanTerakhir.DeleteFailure"),
            _ => result >= 1
        };
    }

    public async Task<ErrorOr<int>> GetBiodataIdForUser(string userId)
    {
        var query = "SELECT Id FROM Biodata WHERE UserId = @UserId";

        using var conn = _context.CreateConnection();
        var biodataId = await conn.QuerySingleOrDefaultAsync<int>(query, new { UserId = userId });
        if (biodataId is 0)
            return Error.NotFound("Biodata.NotFound");

        return biodataId;
    }

    public async Task<bool> IsBiodataExist(int bioId)
    {
        var query = "SELECT 1 FROM Biodata WHERE Id = @BioId";

        using var conn = _context.CreateConnection();
        var result = await conn.QueryFirstOrDefaultAsync<int>(query, new { BioId = bioId });
        return result is 1;
    }
}