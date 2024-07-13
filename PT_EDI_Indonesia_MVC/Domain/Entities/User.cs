using Microsoft.AspNetCore.Identity;

namespace PT_EDI_Indonesia_MVC.Domain.Entities;

public class User : IdentityUser
{
    public string NamaLengkap { get; set; } = string.Empty;
}