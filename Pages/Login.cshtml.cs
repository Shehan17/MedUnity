using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MedUnity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }


        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string SpecialNote { get; set; } = string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string password { get; set; } = string.Empty;

        [BindProperty, Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;



        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var login = new Patient
            {
                FirstName = FirstName,
                LastName = LastName,
                // DateOfBirth =
                DateOfBirth = DateTime.Now,
                Email = Email,
                PasswordHash = password,
                PhoneNumber = PhoneNumber,
                SpecialNote = SpecialNote,

            };

            _context.Patients.Add(login);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }




    }
}
