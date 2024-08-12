using System.Data;
using BiodataManagement.Data.Context;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Web.Service.RiwayatPelatihanService;
using Dapper;
using ErrorOr;

namespace BiodataManagement.Web.Data.Repository;

public class RiwayatPelatihanRepository(DbConnectionFactory dbContext) : IRiwayatPelatihanRepository
{
    private readonly DbConnectionFactory _dbContext = dbContext;

    public async Task<ErrorOr<RiwayatPelatihan>> CreateFor(int biodataId, RiwayatPelatihanCreateRequest request)
    {
        var sql = "usp_RiwayatPelatihan_Insert";
        using var conn = _dbContext.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPelatihan>(
            sql,
            new { request.BiodataId, request.NamaKursus, request.SertifikatAda, request.Tahun },
            commandType: CommandType.StoredProcedure);

        if (result is null)
            return Error.Failure();

        return result;
    }

    public async Task<ErrorOr<RiwayatPelatihan>> Delete(int id)
    {
        var sql = "usp_RiwayatPelatihan_Delete";
        using var conn = _dbContext.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPelatihan>(
            sql,
            new { Id = id },
            commandType: CommandType.StoredProcedure);

        if (result is null)
            return Error.Failure();

        return result;
    }

    public async Task<ErrorOr<List<RiwayatPelatihan>>> GetAllFor(int biodataId)
    {
        var sql = "usp_RiwayatPelatihan_GetByBioId";
        using var conn = _dbContext.CreateConnection();
        var result = await conn.QueryAsync<RiwayatPelatihan>(
            sql,
            new { BiodataId = biodataId },
            commandType: CommandType.StoredProcedure);

        if (result is null || !result.Any())
            return Error.NotFound();

        return result.ToList();
    }

    public async Task<ErrorOr<RiwayatPelatihan>> GetById(int id)
    {
        var sql = "usp_RiwayatPelatihan_GetById";
        using var conn = _dbContext.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPelatihan>(
            sql,
            new { Id = id },
            commandType: CommandType.StoredProcedure);

        if (result is null)
            return Error.NotFound();

        return result;
    }

    public async Task<ErrorOr<RiwayatPelatihan>> Update(int id, RiwayatPelatihan riwayatPelatihan)
    {
        var sql = "usp_RiwayatPelatihan_Update";
        using var conn = _dbContext.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<RiwayatPelatihan>(
            sql,
            new { riwayatPelatihan.Id, riwayatPelatihan.NamaKursus, riwayatPelatihan.SertifikatAda, riwayatPelatihan.Tahun },
            commandType: CommandType.StoredProcedure);

        if (result is null)
            return Error.Failure();

        return result;
    }
}