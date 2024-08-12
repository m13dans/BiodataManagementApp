using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Domain.Entities;

public class PendidikanTerakhir
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    [Display(Name = "Jenjang Pendidikan Terakhir")]
    public string? JenjangPendidikanTerakhir { get; set; }
    [Display(Name = "Nama Institusi Akademik")]
    public string? NamaInstitusiAkademik { get; set; }
    public string? Jurusan { get; set; }
    [Display(Name = "Tahun Lulus")]
    public int TahunLulus { get; set; }
    public float IPK { get; set; }
}