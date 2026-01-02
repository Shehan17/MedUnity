using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MedUnity.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string SpecialNote { get; set; } =string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string password { get; set; } = string.Empty;

        [BindProperty, Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;



        public void OnGet()
        {
        }



    }
}
