namespace PT_EDI_Indonesia_MVC.Core.Models;

public class RiwayatPekerjaan
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    public string? NamaPerusahaan { get; set; }
    public string? PosisiTerakhir { get; set; }
    public decimal PendapatanTerakhir { get; set; }
    public DateOnly Tahun { get; set; }
}