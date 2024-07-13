using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PT_EDI_Indonesia_MVC.Data.Configuration;
using PT_EDI_Indonesia_MVC.Domain.Entities;

namespace PT_EDI_Indonesia_MVC.Data.Context
{
    public class AccountContext : IdentityDbContext<User>
    {
        public AccountContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}