using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Web.Service.RiwayatPekerjaanService;

public record RiwayatPekerjaanCreateRequest
{
    public int BiodataId { get; init; }
    [Display(Name = "Nama Perusahaan")]
    [Required]
    public string? NamaPerusahaan { get; init; }
    [Required]
    [Display(Name = "Posisi Terakhir")]
    public string? PosisiTerakhir { get; init; }
    [Display(Name = "Pendapatan Terakhir")]
    public decimal PendapatanTerakhir { get; init; }
    public int Tahun { get; init; }
}
