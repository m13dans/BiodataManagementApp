using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BiodataManagement.Domain.Entities;

public class RiwayatPekerjaan
{
    [ValidateNever]
    public int Id { get; set; }
    [ValidateNever]
    public int BiodataId { get; set; }
    [Display(Name = "Nama Perusahaan")]
    public string? NamaPerusahaan { get; set; }
    [Display(Name = "Posisi Terakhir")]
    public string? PosisiTerakhir { get; set; }
    [Display(Name = "Pendapatan Terakhir")]
    [Required]
    public decimal PendapatanTerakhir { get; set; }
    [Required]
    public int Tahun { get; set; }
}