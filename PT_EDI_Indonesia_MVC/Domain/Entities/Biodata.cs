using System.ComponentModel.DataAnnotations;

namespace PT_EDI_Indonesia_MVC.Domain.Entities;

public class Biodata
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Posisi Dilamar")]
    public string PosisiDilamar { get; set; } = string.Empty;
    [Required]
    public string Nama { get; set; } = string.Empty;
    [Required]
    [Display(Name = "No KTP")]

    public string NoKTP { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Tempat Lahir")]

    public string TempatLahir { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Tanggal Lahir")]

    public DateTime TanggalLahir { get; set; }
    [Required]
    [Display(Name = "Jenis Kelamin")]

    public JenisKelamin JenisKelamin { get; set; }
    public string? Agama { get; set; } = string.Empty;
    [Display(Name = "Golongan Darah")]
    public string? GolonganDarah { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Alamat Sesuai KTP")]

    public string AlamatKTP { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Alamat Tempat Tinggal")]

    public string AlamatTinggal { get; set; } = string.Empty;
    [Required]

    public string Email { get; set; } = string.Empty;
    [Required]
    [Display(Name = "No Telepon")]

    public string NoTelepon { get; set; } = string.Empty;

    [Display(Name = "Kontak Orang Terdekat")]
    public string? KontakOrangTerdekat { get; set; } = string.Empty;
    public string? Skill { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Bersedia Di Tempatkan di Seluruh Kantor")]
    public bool BersediaDitempatkan { get; set; }

    [Required]
    [Display(Name = "Penghasilan Yang Diharapkan")]
    public decimal PenghasilanDiharapkan { get; set; }
    public string? UserId { get; set; } = string.Empty;
    public ICollection<PendidikanTerakhir>? PendidikanTerakhir { get; set; } = new List<PendidikanTerakhir>();
    public ICollection<RiwayatPekerjaan>? RiwayatPekerjaan { get; set; } = new List<RiwayatPekerjaan>();
    public ICollection<RiwayatPelatihan>? RiwayatPelatihan { get; set; } = new List<RiwayatPelatihan>();

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