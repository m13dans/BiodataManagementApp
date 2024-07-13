using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PT_EDI_Indonesia_MVC.Domain.Entities;

public class User : IdentityUser
{
    public string NamaLengkap { get; set; } = string.Empty;
    [BindNever]
    [ValidateNever()]
    public string? Username { get; set; }

}