using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPCourseProjects.Auth.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed roles (Super Admin, Admin, User)
            // Generate a unique id for each role using this command "Console.WriteLine(Guid.NewGuid())" from View->Other Windows->C# Interactive
            var superAdminRoleId = "fd5702cc - f0d7 - 4990 - a38e - 0a0352bd6dae";
            var adminRoleId = "37d4b8c2-153c-448d-b988-3a7ad9c7a20b";
            var userRoleId = "4d33c6bd-338f-4b09-96f9-5ad977848263";

            // Add roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = superAdminRoleId,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = superAdminRoleId,
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId,
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userRoleId,
                }
            };
            // Add the roles to the model builder
            builder.Entity<IdentityRole>().HasData(roles);

            var superAdminId = "b3b3b3b3-0000-0000-0000-000000000000";
            var superAdminUser = new IdentityUser
            {
                Id = superAdminId,
                UserName = "superadmin",
                Email = "superadmin@email.com",
                NormalizedEmail = "superadmin@email.com".ToUpper(),
                NormalizedUserName = "superadmin".ToUpper(),
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Pass@1234");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to the super admin
            var supserAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(supserAdminRoles);

            base.OnModelCreating(builder);
        }
    }
}
