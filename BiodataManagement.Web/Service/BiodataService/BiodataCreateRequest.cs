using System.ComponentModel.DataAnnotations;
using BiodataManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BiodataManagement.Service.BiodataService;

public class BiodataCreateRequest
{
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

    public DateOnly TanggalLahir { get; set; }
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
    [ValidateNever]
    public string? UserId { get; set; } = string.Empty;

    public ICollection<PendidikanTerakhir>? PendidikanTerakhir { get; set; } = new List<PendidikanTerakhir>();
    public ICollection<RiwayatPekerjaan>? RiwayatPekerjaan { get; set; } = new List<RiwayatPekerjaan>();
    public ICollection<RiwayatPelatihan>? RiwayatPelatihan { get; set; } = new List<RiwayatPelatihan>();
}
