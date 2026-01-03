using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedUnity.Pages
{
    public class MyAppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public MyAppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> MyAppointments { get; set; } = new();

        public async Task OnGetAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email)) return;

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == email);

            if (patient == null) return;

            MyAppointments = await _context.Appointments
                .Where(a => a.PatientId == patient.PatientId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }
    }
}