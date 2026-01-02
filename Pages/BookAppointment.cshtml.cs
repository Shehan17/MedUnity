using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MedUnity.Pages
{

    public class BookAppointmentModel : PageModel
    {

        private readonly AppDbContext _context;

        public BookAppointmentModel(AppDbContext context)
        {
            _context = context;
        }


        [BindProperty, Required]
        public DateTime AppointmentDate { get; set; }

        [BindProperty, Required]
        public string TimeSlot { get; set; } = string.Empty;

        [BindProperty, Required]
        public string DoctorSpecialization { get; set; } = string.Empty;

        [BindProperty]
        public string ReasonForVisit { get; set; } = string.Empty;

        [BindProperty, Required]
        [Phone]
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

            var appointment = new Appointment
            {
                AppointmentDate = AppointmentDate,
                TimeSlot = TimeSlot,
                DoctorSpecialty = DoctorSpecialization,



            };


            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToPage("Index");

        }
    }
}
