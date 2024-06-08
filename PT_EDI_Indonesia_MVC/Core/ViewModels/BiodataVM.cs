namespace PT_EDI_Indonesia_MVC.Core.ViewModels;

public class BiodataVM
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public string TempatLahir { get; set; } = string.Empty;
    public DateTime TanggalLahir { get; set; }
    public string PosisiDilamar { get; set; } = string.Empty;
}