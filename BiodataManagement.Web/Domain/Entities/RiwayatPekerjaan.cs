using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Domain.Entities;

public class RiwayatPekerjaan
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    [Display(Name = "Nama Perusahaan")]
    public string? NamaPerusahaan { get; set; }
    [Display(Name = "Posisi Terakhir")]
    public string? PosisiTerakhir { get; set; }
    [Display(Name = "Pendapatan Terakhir")]
    public decimal PendapatanTerakhir { get; set; }
    public DateTime Tahun { get; set; }
}