using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Service.BiodataService;

public class BiodataDTO
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    [Display(Name = "Tempat Lahir")]
    public string TempatLahir { get; set; } = string.Empty;
    [Display(Name = "Tanggal Lahir")]
    public DateTime TanggalLahir { get; set; }
    [Display(Name = "Posisi Dilamar")]
    public string PosisiDilamar { get; set; } = string.Empty;
}