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
                    FirstName = "Patient",
                    LastName = "Demo",
                    DateOfBirth = new DateTime(1990, 5, 12),
                    Email = "patient@email.com",
                    PhoneNumber = "0771234567",
                    SpecialNote = "Allergic to penicillin",
                    // Give it a temporary value to satisfy the "required" compiler check
                    PasswordHash = ""
                };

                // Now immediately replace that empty string with a real, valid Base-64 hash
                patient.PasswordHash = patientHasher.HashPassword(patient, "patient");

                context.Patients.Add(patient);


                context.SaveChanges();
            }
        }
    }
}
