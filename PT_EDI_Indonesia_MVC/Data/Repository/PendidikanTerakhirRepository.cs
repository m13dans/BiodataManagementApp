using System.Data;
using Dapper;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Data.Context;

namespace PT_EDI_Indonesia_MVC.Data.Repository;

public class PendidikanTerakhirRepository
{
    private readonly DapperContext _context;
    public PendidikanTerakhirRepository(DapperContext context)
    {
        _context = context;

    }
    public async Task<List<PendidikanTerakhir>> GetPendidikanAsync(int biodataId)
    {
        var query = "usp_PendidikanTerakhir_GetByBioId";
        using var connection = _context.CreateConnection();

        var listPendidikan = await connection.QueryAsync<PendidikanTerakhir>(
            query,
            new { bioId = biodataId },
            commandType: CommandType.StoredProcedure);

        // var pendidi = listPendidikan.Select(x => new BiodataVM
        // {
        //     Id = x.Id,
        //     Nama = x.Nama,
        //     TempatLahir = x.TempatLahir,
        //     TanggalLahir = x.TanggalLahir,
        //     PosisiDilamar = x.PosisiDilamar
        // });

        var result = listPendidikan.ToList();
        return result;

    }

    public async Task<List<PendidikanTerakhir>> GetPendidikanWithEmailAsync(string email)
    {
        var query = "usp_PendidikanTerakhir_GetByEmail";
        using var connection = _context.CreateConnection();

        var pendidikans = await connection.QueryAsync<PendidikanTerakhir>(
            query,
            new { Email = email },
            commandType: CommandType.StoredProcedure);

        return pendidikans.ToList();
    }

    internal async Task<bool> CreatePendidikanAsync(PendidikanTerakhir pendidikans, int id)
    {
        var query = "usp_PendidikanTerakhir_Insert";
        using var connection = _context.CreateConnection();

        var pendidikan = await connection.ExecuteAsync(
            query,
            new
            {
                biodataId = id,
                jenjangPendidikanTerakhir = pendidikans.JenjangPendidikanTerakhir,
                namaInstitusiAkademik = pendidikans.NamaInstitusiAkademik,
                jurusan = pendidikans.Jurusan,
                tahunLulus = pendidikans.TahunLulus,
                ipk = pendidikans.IPK
            },
            commandType: CommandType.StoredProcedure);

        return pendidikan > 0;
    }
    internal async Task<bool> UpdatePendidikanAsync(PendidikanTerakhir pendidikans)
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

    internal async Task<bool> DeletePendidikanAsync(int id)
    {
        throw new NotImplementedException();
    }

}