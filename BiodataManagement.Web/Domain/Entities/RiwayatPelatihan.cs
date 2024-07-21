namespace BiodataManagement.Domain.Entities;

public class RiwayatPelatihan
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    public string? NamaKursus { get; set; }
    public bool SertifikatAda { get; set; }
    public DateTime Tahun { get; set; }
}