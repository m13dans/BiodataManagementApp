namespace PT_EDI_Indonesia_MVC.Domain.Entities;

public class AppUserBiodata
{
    public int Id { get; set; }
    public string? AppUserId { get; set; }
    public int BiodataId { get; set; }
    public string Email { get; set; } = string.Empty;
}