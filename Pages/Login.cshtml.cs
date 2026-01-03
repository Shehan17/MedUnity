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
            // Only validate the RegisterData part of the model
            if (!ModelState.IsValid) return Page();

            bool emailExists = await _context.Patients.AnyAsync(u => u.Email == RegisterData.Email);
            if (emailExists)
            {
                ErrorMessage = "Email already registered";
                return Page();
            }

            var hasher = new PasswordHasher<Patient>();
            var newPatient = new Patient
            {
                FirstName = RegisterData.FirstName,
                LastName = RegisterData.LastName,
                DateOfBirth = RegisterData.DOB,
                Email = RegisterData.Email,
                PhoneNumber = RegisterData.PhoneNumber,
                SpecialNote = RegisterData.SpecialNote,
                PasswordHash = "" // Placeholder, will be set below
            };

            // Hash the password before saving!
            newPatient.PasswordHash = hasher.HashPassword(newPatient, RegisterData.Password);

            _context.Patients.Add(newPatient);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        // --- LOGIN HANDLER ---
        public async Task<IActionResult> OnPostLoginAsync()
        {
            // Clear errors from registration fields so login can proceed
            ModelState.ClearValidationState(nameof(RegisterData));

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
                        new Claim("PatientId", patient.PatientId.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToPage("/Index");
                }
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
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