namespace PT_EDI_Indonesia_MVC.Core.Models;

public class Biodata
{
    public int Id { get; set; }
    public string PosisiDilamar { get; set; }
    public string Nama { get; set; }
    public string NoKTP { get; set; }
    public string TempatLahir { get; set; }
    public DateTime TanggalLahir { get; set; }
    public JenisKelamin JenisKelamin { get; set; }
    public string? Agama { get; set; }
    public string? GolonganDarah { get; set; }
    public string? Status { get; set; }
    public string AlamatKTP { get; set; }
    public string AlamatTinggal { get; set; }
    public string Email { get; set; }
    public string NoTelepon { get; set; }
    public string? KontakOrangTerdekat { get; set; }
    public string Skill { get; set; }
    public bool BersediaDitempatkan { get; set; }
    public decimal PenghasilanDiharapkan { get; set; }
    public PendidikanTerakhir? PendidikanTerakhir { get; set; }
    public RiwayatPekerjaan? RiwayatPekerjaan { get; set; }
    public RiwayatPelatihan? RiwayatPelatihan { get; set; }

}

public enum JenisKelamin
{
    Pria,
    Wanita
}

public record PosisiDilamar
{
    public PosisiDilamar()
    {
        Random random = new();
        int number = random.Next(1, 5);

        _posisi = number switch
        {
            1 => "Backend Engineer",
            2 => "Frontend Engineer",
            3 => "Data Analyst",
            4 => "IT Manager",
            _ => "HR"
        };
    }
    private string _posisi;
    public string Posisi
    {
        get => _posisi;
    }
}