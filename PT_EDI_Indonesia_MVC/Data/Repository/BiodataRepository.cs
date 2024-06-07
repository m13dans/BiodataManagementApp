using System.Data;
using Dapper;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Core.ViewModels;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Data.IRepository;
using PT_EDI_Indonesia_MVC.Data.Seed;

namespace PT_EDI_Indonesia_MVC.Data.Repository
{
    public class BiodataRepository : IBiodataRepository
    {
        private readonly DapperContext _context;
        public BiodataRepository(DapperContext context)
        {
            _context = context;

        }

        public async Task<List<BiodataVM>> GetBiodatasAsync()
        {
            var query = "usp_Biodata_SelectForVM";
            using var connection = _context.CreateConnection();

            var biodatas = await connection.QueryAsync<Biodata>(query, CommandType.StoredProcedure);

            var listBiodataVM = biodatas.Select(x => new BiodataVM
            {
                Id = x.Id,
                Nama = x.Nama,
                TempatLahir = x.TempatLahir,
                TanggalLahir = x.TanggalLahir,
                PosisiDilamar = x.PosisiDilamar
            });

            var result = listBiodataVM.ToList();
            return result;

        }

        public async Task<Biodata> GetBiodataAsync(int bioId)
        {
            var query = "usp_Biodata_GetById";
            using var connection = _context.CreateConnection();

            var biodata = await connection.QueryAsync<Biodata, PendidikanTerakhir,
            RiwayatPekerjaan, RiwayatPelatihan, Biodata>(
                query,
                (bio, pedidikanTerakhir, riwayatPekerjaan, riwayatPelatihan) =>
                {
                    bio.PendidikanTerakhir = pedidikanTerakhir;
                    bio.RiwayatPekerjaan = riwayatPekerjaan;
                    bio.RiwayatPelatihan = riwayatPelatihan;

                    return bio;
                },
                param: new { id = bioId },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure);

            return biodata.First();
        }

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
                penghasilanDiharapkan = biodata.PenghasilanDiharapkan
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
    }
}