using MedUnity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedUnity.Data
{
    public static class DbSeeder
    {
        public static void SeedData(AppDbContext context)
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
            }

            if (!context.Patients.Any())
            {
                var patientHasher = new PasswordHasher<Patient>();

                var patient = new Patient
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1990, 5, 12),
                    Email = "john.doe@email.com",
                    PhoneNumber = "0771234567",
                    SpecialNote = "Allergic to penicillin",
                    PasswordHash = "asdasdasdasda",
                    

                    

                };

                context.Patients.Add(patient);
            }

           
            context.SaveChanges();
        }
    }
}
