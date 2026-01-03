using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MedUnity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        // --- Input Models to separate validation ---



        [BindProperty]
        public LoginInput LoginData { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        // --- REGISTRATION HANDLER ---



        // --- LOGIN HANDLER ---
        public async Task<IActionResult> OnPostLoginAsync()
        {



            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == LoginData.Email);

            if (admin != null)
            {
                var hasher = new PasswordHasher<MedUnity.Models.Admin>();
                var result = hasher.VerifyHashedPassword(
                    admin,
                    admin.PasswordHash,
                    LoginData.Password
                );
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, admin.Email),
                        new Claim(ClaimTypes.Email, admin.Email),
                        new Claim("AdminId", admin.AdminId.ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToPage("/ManageAppointments");
                }
            }



            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Email == LoginData.Email);

            if (patient != null)
            {
                var hasher = new PasswordHasher<Patient>();
                var result = hasher.VerifyHashedPassword(patient, patient.PasswordHash, LoginData.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, $"{patient.FirstName} {patient.LastName}"),
                        new Claim(ClaimTypes.Email, patient.Email),
                        new Claim("PatientId", patient.PatientId.ToString()),
                        new Claim(ClaimTypes.Role, "Patient")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToPage("/Index");
                }
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

        // --- Helper Classes for Validation ---


        public class LoginInput
        {
            [Required, EmailAddress] public string Email { get; set; }
            [Required] public string Password { get; set; }
        }
    }
}