namespace PT_EDI_Indonesia_MVC.Core.Models;

public class RiwayatPelatihan
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    public string? NamaKursus { get; set; }
    public bool SertifikatAda { get; set; }
    public DateOnly Tahun { get; set; }
}