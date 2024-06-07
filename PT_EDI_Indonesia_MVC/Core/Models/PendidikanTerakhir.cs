namespace PT_EDI_Indonesia_MVC.Core.Models;

public class PendidikanTerakhir
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    public string? JenjangPendidikanTerakhir { get; set; }
    public string? NamaInstitusiAkademik { get; set; }
    public string Jurusan { get; set; }
    public DateTime TahunLulus { get; set; }
    public float IPK { get; set; }
}