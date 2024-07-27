using System.Data;
using BiodataManagement.Service.BiodataService;
using BiodataManagement.Service.PendidikanTerakhirService;

namespace BiodataManagement.Data.Repository;

public static class TableHelper
{
    public static DataTable CreateBiodataTable()
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
        return tvpBiodata;
    }
    public static DataTable CreateBiodataTable(BiodataCreateRequest request)
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
            request.PosisiDilamar,
            request.Nama,
            request.NoKTP,
            request.TempatLahir,
            request.TanggalLahir.ToDateTime(new TimeOnly(0, 0)),
            request.JenisKelamin,
            request.Agama,
            request.GolonganDarah,
            request.Status,
            request.AlamatKTP,
            request.AlamatTinggal,
            request.Email,
            request.NoTelepon,
            request.KontakOrangTerdekat,
            request.Skill,
            request.BersediaDitempatkan,
            request.PenghasilanDiharapkan,
            request.UserId);

        return tvpBiodata;
    }
    public static DataTable CreatePendidikanTerakhirTable()
    {
        var tvpPendidikan = new DataTable();
        tvpPendidikan.Columns.Add("BiodataId", typeof(int));
        tvpPendidikan.Columns.Add("JenjangPendidikanTerakhir", typeof(string));
        tvpPendidikan.Columns.Add("NamaInstitusiAkademik", typeof(string));
        tvpPendidikan.Columns.Add("Jurusan", typeof(string));
        tvpPendidikan.Columns.Add("TahunLulus", typeof(int));
        tvpPendidikan.Columns.Add("IPK", typeof(float));
        return tvpPendidikan;
    }
    public static DataTable CreatePendidikanTerakhirTable(IEnumerable<PendidikanTerakhirRequest> requests)
    {
        var pendidikanTable = new DataTable();

        pendidikanTable.Columns.Add("BiodataId", typeof(int));
        pendidikanTable.Columns.Add("JenjangPendidikanTerakhir", typeof(string));
        pendidikanTable.Columns.Add("NamaInstitusiAkademik", typeof(string));
        pendidikanTable.Columns.Add("Jurusan", typeof(string));
        pendidikanTable.Columns.Add("TahunLulus", typeof(int));
        pendidikanTable.Columns.Add("IPK", typeof(float));

        foreach (var request in requests)
        {
            pendidikanTable.Rows.Add(request.BiodataId);
            pendidikanTable.Rows.Add(request.JenjangPendidikanTerakhir);
            pendidikanTable.Rows.Add(request.NamaInstitusiAkademik);
            pendidikanTable.Rows.Add(request.Jurusan);
            pendidikanTable.Rows.Add(request.TahunLulus);
            pendidikanTable.Rows.Add(request.IPK);
        }
        return pendidikanTable;
    }
}