using MedUnity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedUnity.Data
{
    public static class DbSeeder
    {
        public static void SeedAdmins(AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.Admin.Any())
            {
                var passwordHasher = new PasswordHasher<Admin>();

                var admin = new Admin
                {
                    Email = "admin@email.com"
                };

                admin.PasswordHash = passwordHasher.HashPassword(admin, "admin");

                context.Admin.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
