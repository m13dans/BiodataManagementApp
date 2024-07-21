using System.Data;
using Dapper;
using ErrorOr;
using BiodataManagement.Data.Context;
using BiodataManagement.Domain.Entities;
using BiodataManagement.Service.BiodataService;

namespace BiodataManagement.Data.Repository;

public class BiodataRepository : IBiodataRepository
{
    private readonly DapperContext _context;
    private readonly IHttpContextAccessor _httpContext;
    public BiodataRepository(DapperContext context, IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
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

    public async Task<List<PendidikanTerakhir>> GetPendidikansAsync(int biodataId)
    {
        var query = "usp_PendidikanTerakhir_GetByBioId";
        using var connection = _context.CreateConnection();

        var pendidikans = await connection.QueryAsync<PendidikanTerakhir>(query, new { bioId = biodataId },
        commandType: CommandType.StoredProcedure);

        return pendidikans.ToList();
    }

    public async Task<bool> UpdateBiodataAsync(int biodataId, Biodata biodata)
    {
        var query = "usp_Biodata_Update";
        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            id = biodataId,
            posisiDilamar = biodata.PosisiDilamar,
            nama = biodata.Nama,
            noKTP = biodata.NoKTP,
            tempatLahir = biodata.TempatLahir,
            tanggalLahir = biodata.TanggalLahir,
            jenisKelamin = biodata.JenisKelamin,
            agama = biodata.Agama,
            golonganDarah = biodata.GolonganDarah,
            status = biodata.Status,
            alamatKtp = biodata.AlamatKTP,
            alamatTinggal = biodata.AlamatTinggal,
            email = biodata.Email,
            noTelepon = biodata.NoTelepon,
            kontakOrangTerdekat = biodata.KontakOrangTerdekat,
            skill = biodata.Skill,
            bersediaDitempatkan = biodata.BersediaDitempatkan,
            penghasilanDiharapkan = biodata.PenghasilanDiharapkan,
            userId = biodata.UserId

        },
        commandType: CommandType.StoredProcedure);

        return result > 0;
    }
    public async Task<bool> UpdateBiodataByAdminAsync(int biodataId, Biodata biodata)
    {
        var query = "usp_Biodata_UpdateByAdmin";
        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            id = biodataId,
            posisiDilamar = biodata.PosisiDilamar,
            nama = biodata.Nama,
            noKTP = biodata.NoKTP,
            tempatLahir = biodata.TempatLahir,
            tanggalLahir = biodata.TanggalLahir,
            jenisKelamin = biodata.JenisKelamin,
            agama = biodata.Agama,
            golonganDarah = biodata.GolonganDarah,
            status = biodata.Status,
            alamatKtp = biodata.AlamatKTP,
            alamatTinggal = biodata.AlamatTinggal,
            email = biodata.Email,
            noTelepon = biodata.NoTelepon,
            kontakOrangTerdekat = biodata.KontakOrangTerdekat,
            skill = biodata.Skill,
            bersediaDitempatkan = biodata.BersediaDitempatkan,
            penghasilanDiharapkan = biodata.PenghasilanDiharapkan,
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

    public async Task<bool> CreateBiodataAsync(Biodata biodata)
    {

        var tvpBiodata = new DataTable();
        tvpBiodata.Columns.Add("PosisiDilamar", typeof(string));
        tvpBiodata.Columns.Add("Nama", typeof(string));
        tvpBiodata.Columns.Add("NoKTP", typeof(string));
        tvpBiodata.Columns.Add("TempatLahir", typeof(string));
        tvpBiodata.Columns.Add("TanggalLahir", typeof(DateTime));
        tvpBiodata.Columns.Add("JenisKelamin", typeof(int));
        tvpBiodata.Columns.Add("Agama", typeof(string));
        tvpBiodata.Columns.Add("GolonganDarah", typeof(string));
        tvpBiodata.Columns.Add("Status", typeof(string));
        tvpBiodata.Columns.Add("AlamatKtp", typeof(string));
        tvpBiodata.Columns.Add("AlamatTinggal", typeof(string));
        tvpBiodata.Columns.Add("Email", typeof(string));
        tvpBiodata.Columns.Add("NoTelepon", typeof(string));
        tvpBiodata.Columns.Add("KontakOrangTerdekat", typeof(string));
        tvpBiodata.Columns.Add("Skill", typeof(string));
        tvpBiodata.Columns.Add("BersediaDitempatkan", typeof(bool));
        tvpBiodata.Columns.Add("PenghasilanDiHarapkan", typeof(decimal));
        tvpBiodata.Columns.Add("UserId", typeof(string));

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

    public async Task<int> GetCurrentUserId(string email)
    {
        var query = "SELECT DISTINCT Id FROM Biodata WHERE EMAIL = @Email";
        using var connection = _context.CreateConnection();

        var userId = await connection.QuerySingleOrDefaultAsync<int>(query, new { Email = email });
        return userId;
    }

    public async Task<string> GetBiodataOwnerId(string userId)
    {
        var query = "SELECT AppUserId FROM AppUserBiodata WHERE AppUserId = @UserId";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<string>(query, new { UserId = userId });

        return result ?? string.Empty;
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

    public async Task<bool> ValidateBiodataOwner(string userEmail)
    {
        var query = "SELECT Email FROM Biodata WHERE Email = @UserEmail";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<string>(query, new { UserEmail = userEmail });

        return result is not null;
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
        var query = "SELECT 1 FROM Biodata WHERE UserId = @UserId OR Email = @Email;";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<int>(query, new { UserId = userId, Email = userEmail });

        return result is 1;
    }
}