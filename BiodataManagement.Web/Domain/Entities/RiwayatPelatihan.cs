using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Domain.Entities;

public class RiwayatPelatihan
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    [Display(Name = "Nama Kursus")]
    public string? NamaKursus { get; set; }
    [Display(Name = "Sertifikat")]
    public bool SertifikatAda { get; set; }
    public DateTime Tahun { get; set; }
}