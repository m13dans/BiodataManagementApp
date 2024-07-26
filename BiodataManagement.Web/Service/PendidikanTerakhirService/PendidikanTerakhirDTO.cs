using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    public DateTime TahunLulus { get; set; }
    public float IPK { get; set; }
}