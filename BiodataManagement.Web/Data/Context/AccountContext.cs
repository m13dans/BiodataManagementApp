using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BiodataManagement.Data.Configuration;
using BiodataManagement.Data.Identity;

namespace BiodataManagement.Data.Context
{
    public class AccountContext : IdentityDbContext<AppUser>
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