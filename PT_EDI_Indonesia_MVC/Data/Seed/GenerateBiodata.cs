using System.Data;
using Bogus;
using Bogus.Extensions;
using Dapper;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Domain.Entities;

namespace PT_EDI_Indonesia_MVC.Data.Seed;

public class GenerateData
{
    private readonly DapperContext _context;
    public GenerateData(DapperContext context)
    {
        _context = context;
    }
    public static List<Biodata> GenerateBiodata()
    {
        var faker = new Faker<Biodata>()
            .StrictMode(false)
            .Rules((f, p) =>
            {
                p.PosisiDilamar = f.Random.ArrayElement(new string[]
                {
                    "Backend Engineer",
                    "Frontend Engineer",
                    "Data Analyst",
                    "IT Manager",
                    "HR"
                });
                p.Nama = f.Person.FullName.ClampLength(1, 100);
                p.NoKTP = f.Random.Guid().ToString();
                p.TempatLahir = f.Address.City();
                p.TanggalLahir = f.Person.DateOfBirth;

                p.JenisKelamin = (JenisKelamin)f.Random.Int(1, 2);
                p.Agama = f.Random.ArrayElement(new string[]
                {
                    "Islam", "Kristen Protestan", "Budha", "Hindu", "Kristen Katolik"
                });
                p.GolonganDarah = f.Random.ArrayElement(new string[] { "A", "B", "AB", "O" });
                p.Status = f.Random.ArrayElement(new string[] { "Menikah", "Single" });
                p.AlamatKTP = f.Person.Address.Street;
                p.AlamatTinggal = f.Person.Address.Street;
                p.Email = f.Person.Email;
                p.NoTelepon = f.Person.Phone;
                p.KontakOrangTerdekat = f.Phone.PhoneNumber().OrNull(f, 0.2f);
                p.Skill = f.Random.ArrayElement(new string[]
                {
                    "C#, HTML, CSS, Javascript, Angular, SQL",
                    "Java, HTML, CSS, SQL, React",
                    "Python, SQL, Power, BI",
                    "Leadership, Communication, Mentoring, Agile",
                    "Microsoft Word, Microsoft Excell, Powerpoint"
                });
                p.BersediaDitempatkan = f.Random.Bool();
                p.PenghasilanDiharapkan = f.Random.Decimal2(5_000_000, 15_000_000);
            });

        var biodatas = faker.Generate(50);
        return biodatas;

    }

    public async Task<int> SubmitBiodata()
    {
        var listBiodata = GenerateBiodata();

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

        foreach (var biodata in listBiodata)
        {
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
        }

        using var conn = _context.CreateConnection();

        var tvp = tvpBiodata.AsTableValuedParameter("UDT_Biodata");

        var result = await conn.ExecuteAsync(
            "usp_Biodata_BulkInsert",
            new { udtBiodata = tvp },
            commandType: CommandType.StoredProcedure);

        return result;
    }
}
