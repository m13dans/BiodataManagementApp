namespace BiodataManagement.Domain.Entities;

public class RiwayatPekerjaan
{
    public int Id { get; set; }
    public int BiodataId { get; set; }
    public string? NamaPerusahaan { get; set; }
    public string? PosisiTerakhir { get; set; }
    public decimal PendapatanTerakhir { get; set; }
    public DateTime Tahun { get; set; }
}