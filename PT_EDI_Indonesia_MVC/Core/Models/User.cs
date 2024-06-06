using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PT_EDI_Indonesia_MVC.Core.Models
{
    public class User : IdentityUser
    {
        public string? NamaLengkap { get; set; }
    }
}