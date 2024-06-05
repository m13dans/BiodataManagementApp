namespace PT_EDI_Indonesia_MVC.Core.Models;

public class PendidikanTerakhir
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    public string? JenjangPendidikanTerakhir { get; set; }
    public string? NamaInstitusiAkademik { get; set; }
    public decimal Jurusan { get; set; }
    public DateOnly TahunLulus { get; set; }
    public float IPK { get; set; }
}