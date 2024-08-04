using System.Data;
using BiodataManagement.Data.Context;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Web.Service.RiwayatPekerjaanService;
using Dapper;
using ErrorOr;

namespace BiodataManagement.Web.Data.Repository;

public class RiwayatPekerjaanRepository : IRiwayatPekerjaanRepository
{
    private readonly DbConnectionFactory _dbContext;
    public RiwayatPekerjaanRepository(DbConnectionFactory dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<RiwayatPekerjaan>> CreateRiwayatPekerjaan(int biodataId, RiwayatPekerjaanCreateRequest request)
    {
        var sql = "usp_RiwayatPekerjaan_Insert";

        using var conn = _dbContext.CreateConnection();

        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPekerjaan>(sql,
        new
        {
            BiodataId = biodataId,
            request.NamaPerusahaan,
            request.PosisiTerakhir,
            request.PendapatanTerakhir,
            request.Tahun
        }, commandType: CommandType.StoredProcedure);

        if (result is null)
        {
            return Error.Failure();
        }

        return result;
    }

    public async Task<ErrorOr<RiwayatPekerjaan>> DeleteRiwayatPekerjaanById(int id)
    {
        var sql = "usp_RiwayatPekerjaan_Delete";

        using var conn = _dbContext.CreateConnection();

        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPekerjaan>(
            sql,
            new { Id = id },
            commandType: CommandType.StoredProcedure);

        if (result is null)
            return Error.Failure();

        return result;
    }

    public async Task<List<RiwayatPekerjaan>> GetAllRiwayatPekerjaanFor(int biodataId)
    {
        var sql = "usp_RiwayatPekerjaan_GetByBioId";

        using var conn = _dbContext.CreateConnection();

        var result = await conn.QueryAsync<RiwayatPekerjaan>(
            sql,
            new { BiodataId = biodataId },
            commandType: CommandType.StoredProcedure);


        return result is null ? [] : result.ToList();
    }

    public async Task<ErrorOr<RiwayatPekerjaan>> GetRiwayatPekerjaanById(int id)
    {
        var sql = "usp_RiwayatPekerjaan_GetById";

        using var conn = _dbContext.CreateConnection();

        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPekerjaan>(
            sql,
            new { Id = id },
            commandType: CommandType.StoredProcedure);


        return result is null ? Error.NotFound() : result;

    }

    public async Task<ErrorOr<RiwayatPekerjaan>> UpdateRiwayatPekerjaan(int id, RiwayatPekerjaan riwayatPekerjaan)
    {
        var sql = "usp_RiwayatPekerjaan_Delete";

        using var conn = _dbContext.CreateConnection();

        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPekerjaan>(
            sql,
            new { Id = id },
            commandType: CommandType.StoredProcedure);


        return result is null ? Error.Failure() : result;
    }
}
