using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedUnity.Pages
{
    public class MyAppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public MyAppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

        public Appointment? MyAppointment { get; set; }

        public async Task OnGetAsync()
        {

            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return;

          
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == username); 
            if (patient == null) return;

         
            MyAppointment = await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patient.PatientId)
                .OrderByDescending(a => a.AppointmentDate)
                .FirstOrDefaultAsync();
        }
    }
}
