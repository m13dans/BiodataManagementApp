using System.ComponentModel.DataAnnotations;

namespace BiodataManagement.Service.PendidikanTerakhirService;

public record PendidikanTerakhirRequest
{
    public int BiodataId { get; set; }
    [MaxLength(50)]
    public string? JenjangPendidikanTerakhir { get; set; }
    [MaxLength(255)]
    public string? NamaInstitusiAkademik { get; set; }
    [MaxLength(255)]
    public string? Jurusan { get; set; }
    public DateOnly TahunLulus { get; set; }
    public float IPK { get; set; }
}
