using System.Data;
using Dapper;
using ErrorOr;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.BiodataService;

namespace PT_EDI_Indonesia_MVC.Data.Repository;

public class BiodataRepository : IBiodataRepository
{
    private readonly DapperContext _context;
    public BiodataRepository(DapperContext context)
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

    public async Task<Biodata> GetBiodataAsync(int bioId)
    {
        var query = "usp_Biodata_GetById";
        using var connection = _context.CreateConnection();
        connection.Open();

        Biodata biodata;
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
            param: new { id = bioId },
            splitOn: "Id",
            commandType: CommandType.StoredProcedure);

        biodata = biodatas.GroupBy(x => x.Id).Select(x =>
        {
            Biodata bio = x.First();
            bio.PendidikanTerakhir = x.SelectMany(y => y.PendidikanTerakhir).ToList();
            bio.RiwayatPekerjaan = x.SelectMany(y => y.RiwayatPekerjaan).ToList();
            bio.RiwayatPelatihan = x.SelectMany(y => y.RiwayatPelatihan).ToList();
            return bio;
        }).FirstOrDefault();

        return biodata;
    }

    public async Task<Biodata> GetBiodataWithEmailAsync(string userEmail)
    {
        var query = "usp_Biodata_GetByEmail";
        using var connection = _context.CreateConnection();
        connection.Open();

        Biodata biodata;
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

        biodata = biodatas.GroupBy(x => x.Id).Select(x =>
        {
            Biodata bio = x.First();
            bio.PendidikanTerakhir = x.SelectMany(y => y.PendidikanTerakhir).ToList();
            bio.RiwayatPekerjaan = x.SelectMany(y => y.RiwayatPekerjaan).ToList();
            bio.RiwayatPelatihan = x.SelectMany(y => y.RiwayatPelatihan).ToList();
            return bio;
        }).FirstOrDefault();

        return biodata;
    }
    // public async Task<BiodataWithChild> GetBiodataWithRelationAsync(int bioId)
    // {
    //     var query = "usp_Biodata_GetById";
    //     using var connection = _context.CreateConnection();

    //     var biodata = await connection.QueryAsync<Biodata, PendidikanTerakhir,
    //     RiwayatPekerjaan, RiwayatPelatihan, Biodata>(
    //         query,
    //         (bio, pedidikanTerakhir, riwayatPekerjaan, riwayatPelatihan) =>
    //         {
    //             bio.PendidikanTerakhir = pedidikanTerakhir;
    //             bio.RiwayatPekerjaan = riwayatPekerjaan;
    //             bio.RiwayatPelatihan = riwayatPelatihan;

    //             return bio;
    //         },
    //         param: new { id = bioId },
    //         splitOn: "Id",
    //         commandType: CommandType.StoredProcedure);

    //     var bioWithRelation = biodata.Select(x => new BiodataWithChild
    //     {
    //         Id = x.Id,
    //         PosisiDilamar = x.PosisiDilamar
    //     });

    //     return biodata.First();
    // }

    public async Task<List<PendidikanTerakhir>> GetPendidikansAsync(int biodataId)
    {
        var query = "usp_PendidikanTerakhir_GetByBioId";
        using var connection = _context.CreateConnection();

        var pendidikans = await connection.QueryAsync<PendidikanTerakhir>(query, new { bioId = biodataId },
        commandType: CommandType.StoredProcedure);

        return pendidikans.ToList();
    }

    public async Task<bool> UpdateBiodataAsync(Biodata biodata)
    {
        var query = "usp_Biodata_Update";
        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            id = biodata.Id,
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

        return result > 0;
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

        tvpBiodata.Rows.Add(biodata.PosisiDilamar,
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
            biodata.PenghasilanDiharapkan);

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

    public async Task<string> GetBiodataOwnerId(Guid userId)
    {
        var query = "SELECT UserId FROM Biodata WHERE UserId = @UserId";
        using var connection = _context.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<string>(query, new { UserId = userId });

        return result ?? string.Empty;
    }
}