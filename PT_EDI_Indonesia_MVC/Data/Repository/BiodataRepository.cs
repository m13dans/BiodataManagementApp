using Dapper;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Core.ViewModels;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Data.IRepository;

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
            var query = "SELECT * FROM Biodata";
            using var connection = _context.CreateConnection();

            var biodatas = await connection.QueryAsync<Biodata>(query);

            var listBiodataVM = biodatas.Select(x => new BiodataVM
            {
                Nama = x.Nama,
                TempatLahir = x.TempatLahir,
                TanggalLahir = x.TanggalLahir,
                PosisiDilamar = x.PosisiDilamar
            });

            var result = listBiodataVM.ToList();
            return result;

        }

    }
}