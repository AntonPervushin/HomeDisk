using HomeDisk.Api.Authentication;
using HomeDisk.Api.Common.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeDisk.Api.Persistence
{
    public class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            foreach (var role in (AppIdentityRole[])Enum.GetValues(typeof(AppIdentityRole)))
            {
                modelBuilder.Entity<AppRole>().HasData(
                    new AppRole { 
                        Id = (int)role, 
                        Name = role.ToString(),
                        NormalizedName = role.ToString().ToUpper()
                    });
            }

            var hasher = new PasswordHasher<IdentityUser>();
            var adminName = "admin";
            modelBuilder.Entity<AppUser>().HasData(new AppUser 
            { 
                Id = 1, 
                UserName = adminName,
                NormalizedUserName = adminName.ToUpper(),
                PasswordHash = hasher.HashPassword(null, "admin"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = (int)AppIdentityRole.Admin,
                    UserId = 1
                }
            );
        }
    }
}
