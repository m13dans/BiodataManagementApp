using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BiodataManagement.Data.Identity;

public class AppUser : IdentityUser
{
    public string NamaLengkap { get; set; } = string.Empty;
    [BindNever]
    [ValidateNever()]
    public override string? UserName { get; set; }

}