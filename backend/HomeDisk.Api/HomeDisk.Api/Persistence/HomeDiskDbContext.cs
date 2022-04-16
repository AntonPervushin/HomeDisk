using HomeDisk.Api.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeDisk.Api.Persistence
{
    public class HomeDiskDbContext
        : IdentityDbContext<AppUser, AppRole, int>
    {
        public HomeDiskDbContext(DbContextOptions<HomeDiskDbContext> Options)
            : base(Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().Property(p => p.Id).UseIdentityColumn();

            DataSeeder.SeedData(builder);
        }
    }
}
