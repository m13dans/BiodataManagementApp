using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Service.PendidikanTerakhirService;

public record PendidikanTerakhirRequest
{
    public int BiodataId { get; set; }
    [MaxLength(50)]
    [DisplayName("Jenjang Pendidikan Terakhir")]
    [Required]
    public string? JenjangPendidikanTerakhir { get; set; }
    [MaxLength(255)]
    [DisplayName("Nama Institusi Akademik")]
    [Required]
    public string? NamaInstitusiAkademik { get; set; }
    [MaxLength(255)]
    [Required]

    public string? Jurusan { get; set; }
    [DisplayName("Tahun Lulus")]
    [Required]
    public int TahunLulus { get; set; }
    public float IPK { get; set; }
}
