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
        public RegisterInput RegisterData { get; set; }

        [BindProperty]
        public LoginInput LoginData { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        // --- REGISTRATION HANDLER ---
        public async Task<IActionResult> OnPostRegisterAsync()
        {

            ModelState.Remove("LoginData.Email");
            ModelState.Remove("LoginData.Password");

            if (!ModelState.IsValid)
                return Page();

            bool emailExists = await _context.Patients
                .AnyAsync(p => p.Email == RegisterData.Email);

            if (emailExists)
            {
                ErrorMessage = "Email already registered";
                return Page();
            }

            var hasher = new PasswordHasher<Patient>();

            var patient = new Patient
            {
                FirstName = RegisterData.FirstName,
                LastName = RegisterData.LastName,
                DateOfBirth = RegisterData.DOB,
                Email = RegisterData.Email,
                PhoneNumber = RegisterData.PhoneNumber,
                SpecialNote = RegisterData.SpecialNote,
                PasswordHash = hasher.HashPassword(null, RegisterData.Password)
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }


        // --- LOGIN HANDLER ---
        public async Task<IActionResult> OnPostLoginAsync()
        {

            ModelState.ClearValidationState(nameof(RegisterData));

            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == LoginData.Email);

            if (admin != null)
            {
                var hasher = new PasswordHasher<Admin>();
                var result = hasher.VerifyHashedPassword(admin, admin.PasswordHash, LoginData.Password);
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
        public class RegisterInput
        {
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required, EmailAddress] public string Email { get; set; }
            [Required, MinLength(6)] public string Password { get; set; }
            [Required] public string PhoneNumber { get; set; }
            [Required] public DateTime DOB { get; set; }
            public string SpecialNote { get; set; }
        }

        public class LoginInput
        {
            [Required, EmailAddress] public string Email { get; set; }
            [Required] public string Password { get; set; }
        }
    }
}