
using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Web.Service.RiwayatPelatihanService;

public class RiwayatPelatihanCreateRequest
{
    public int BiodataId { get; set; }
    [Display(Name = "Nama Kursus")]
    [Required]
    public string? NamaKursus { get; set; }
    [Display(Name = "Sertifikat")]
    public bool SertifikatAda { get; set; } = false;
    public int Tahun { get; set; }
}
