using System.Data;
using Dapper;
using ErrorOr;
using BiodataManagement.Data.Context;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Service.BiodataService;

namespace BiodataManagement.Data.Repository;

public class BiodataRepository : IBiodataRepository
{
    private readonly DbConnectionFactory _context;
    public BiodataRepository(DbConnectionFactory context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<BiodataDTO>>> GetBiodataListAsync()
    {
        var query = "usp_Biodata_SelectForDTO";
        using var connection = _context.CreateConnection();

        var biodataDTOs = await connection.QueryAsync<BiodataDTO>(query, CommandType.StoredProcedure);

        if (biodataDTOs.Count() <= 0)
        {
            return Error.NotFound(code: "Biodata.NotFound", description: "Biodata List is Empty");
        }

        var result = biodataDTOs.ToList();
        return result;
    }

    public async Task<ErrorOr<Biodata>> GetBiodataByIdAsync(int bioId)
    {
        string query = "usp_Biodata_GetById";
        using var connection = _context.CreateConnection();

        IEnumerable<Biodata>? biodatas = await connection.QueryAsync<Biodata, PendidikanTerakhir,
        RiwayatPekerjaan, RiwayatPelatihan, Biodata>(
            query,
            (bio, pendidikanTerakhir, riwayatPekerjaan, riwayatPelatihan) =>
            {
                if (pendidikanTerakhir is not null)
                    bio.PendidikanTerakhir!.Add(pendidikanTerakhir);
                if (riwayatPekerjaan is not null)
                    bio.RiwayatPekerjaan!.Add(riwayatPekerjaan);
                if (riwayatPelatihan is not null)
                    bio.RiwayatPelatihan!.Add(riwayatPelatihan);
                return bio;
            },
            param: new { id = bioId },
            splitOn: "Id",
            commandType: CommandType.StoredProcedure);

        if (biodatas is null)
            return Error.NotFound("Biodata.NotFound");

        var biodata = biodatas.GroupBy(x => x.Id).Select(x =>
        {
            Biodata bio = x.First();
            bio.PendidikanTerakhir = x.SelectMany(y => y.PendidikanTerakhir ?? Enumerable.Empty<PendidikanTerakhir>()).ToList();
            bio.RiwayatPekerjaan = x.SelectMany(y => y.RiwayatPekerjaan ?? Enumerable.Empty<RiwayatPekerjaan>()).ToList();
            bio.RiwayatPelatihan = x.SelectMany(y => y.RiwayatPelatihan ?? Enumerable.Empty<RiwayatPelatihan>()).ToList();
            return bio;
        }).FirstOrDefault();

        if (biodata is null)
            return Error.NotFound("Biodata.NotFound");

        return biodata;
    }

    public async Task<ErrorOr<Biodata>> GetBiodataWithEmailAsync(string userEmail)
    {
        var query = "usp_Biodata_GetByEmail";
        using var connection = _context.CreateConnection();

        var biodatas = await connection.QueryAsync<Biodata, PendidikanTerakhir,
        RiwayatPekerjaan, RiwayatPelatihan, Biodata>(
            query,
            (bio, pendidikanTerakhir, riwayatPekerjaan, riwayatPelatihan) =>
            {
                if (pendidikanTerakhir is not null)
                    bio.PendidikanTerakhir!.Add(pendidikanTerakhir);
                if (riwayatPekerjaan is not null)
                    bio.RiwayatPekerjaan!.Add(riwayatPekerjaan);
                if (riwayatPelatihan is not null)
                    bio.RiwayatPelatihan!.Add(riwayatPelatihan);
                return bio;
            },
            param: new { email = userEmail },
            splitOn: "Id",
            commandType: CommandType.StoredProcedure);

        if (biodatas is null)
            return Error.NotFound("Biodata.NotFound");

        var biodata = biodatas.GroupBy(x => x.Id).Select(x =>
        {
            Biodata bio = x.First();
            bio.PendidikanTerakhir = x.SelectMany(y => y.PendidikanTerakhir ?? Enumerable.Empty<PendidikanTerakhir>()).ToList();
            bio.RiwayatPekerjaan = x.SelectMany(y => y.RiwayatPekerjaan ?? Enumerable.Empty<RiwayatPekerjaan>()).ToList();
            bio.RiwayatPelatihan = x.SelectMany(y => y.RiwayatPelatihan ?? Enumerable.Empty<RiwayatPelatihan>()).ToList();
            return bio;
        }).FirstOrDefault();

        if (biodata is null)
            return Error.NotFound("Biodata.NotFound");

        return biodata;
    }

    public async Task<bool> UpdateBiodataAsync(int biodataId, BiodataUpdateRequest biodata)
    {
        var query = "usp_Biodata_Update";
        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            id = biodataId,
            biodata.PosisiDilamar,
            biodata.Nama,
            biodata.TempatLahir,
            biodata.TanggalLahir,
            biodata.JenisKelamin,
            biodata.Agama,
            biodata.GolonganDarah,
            biodata.Status,
            biodata.AlamatKTP,
            biodata.AlamatTinggal,
            biodata.Email,
            biodata.NoTelepon,
            biodata.KontakOrangTerdekat,
            biodata.Skill,
            biodata.BersediaDitempatkan,
            biodata.PenghasilanDiharapkan,
        },
        commandType: CommandType.StoredProcedure);

        return result is 1;
    }
    public async Task<bool> DeleteBiodataAsync(int bioId)
    {
        var query = "usp_Biodata_Delete";
        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteAsync(
            query,
            new { id = bioId },
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }

    public async Task<bool> CreateListBiodataAsync(Biodata biodata)
    {
        DataTable tvpBiodata = TableHelper.CreateBiodataTable();

        tvpBiodata.Rows.Add(
            biodata.PosisiDilamar,
            biodata.Nama,
            biodata.NoKTP,
            biodata.TempatLahir,
            biodata.TanggalLahir,
            biodata.JenisKelamin,
            biodata.Agama,
            biodata.GolonganDarah,
            biodata.Status,
            biodata.AlamatKTP,
            biodata.AlamatTinggal,
            biodata.Email,
            biodata.NoTelepon,
            biodata.KontakOrangTerdekat,
            biodata.Skill,
            biodata.BersediaDitempatkan,
            biodata.PenghasilanDiharapkan,
            biodata.UserId);

        using var conn = _context.CreateConnection();

        var tvp = tvpBiodata.AsTableValuedParameter("dbo.UDT_Biodata");

        var result = await conn.ExecuteAsync(
            "dbo.usp_Biodata_BulkInsert",
            new { udtBiodata = tvp },
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }

    public async Task<ErrorOr<Biodata>> CreateBiodataAsync(BiodataCreateRequest request)
    {
        var query = "usp_Biodata_Create";


        var bioTable = TableHelper.CreateBiodataTable(request);
        var tvp = bioTable.AsTableValuedParameter("UDT_Biodata");

        using var conn = _context.CreateConnection();


        var result = await conn.QuerySingleOrDefaultAsync<Biodata>(query, new { udtBiodata = tvp });

        if (result is null)
            return Error.Failure("Biodata.Failure");

        return result;
    }

    public async Task<ErrorOr<Biodata>> GetBiodataWithUserId(string userId)
    {
        var query = "SELECT * FROM Biodata WHERE UserId = @UserId ";

        using var connection = _context.CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<Biodata>(query, new { UserId = userId });

        if (result is null)
            return Error.NotFound("Biodata.NotFound");

        return result;
    }

    public async Task<ErrorOr<AppUserBiodata>> GetAppUserBiodataAsync(string userId)
    {
        var query = "SELECT * FROM AppUserBiodata WHERE AppUserId = @AppUserId";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<AppUserBiodata>(query, new { AppUserId = userId });

        if (result is null)
            return Error.NotFound("AppUserBiodata.NotFound");

        return result;
    }

    public async Task<bool> IsBiodataExist(string userId, string userEmail)
    {
        var query = "SELECT 1 FROM Biodata WHERE UserId = @UserId OR Email = @Email";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<int>(query, new { UserId = userId, Email = userEmail });

        return result is 1;
    }

    public async Task<bool> IsKTPExists(string ktp)
    {
        var query = "SELECT 1 FROM Biodata WHERE NoKTP = @NoKTP";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<int>(query, new { NoKTP = ktp });

        return result is 1;
    }
}
