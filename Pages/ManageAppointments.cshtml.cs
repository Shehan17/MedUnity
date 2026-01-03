using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedUnity.Pages.Admin
{
    public class ManageAppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ManageAppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

       
        public List<Appointment> Appointments { get; set; } = new();

        [BindProperty]
        public string? RejectedReason { get; set; }

        public async Task OnGetAsync()
        {
            Appointments = await _context.Appointments
                .Include(a => a.Patient) 
                .OrderByDescending(a => a.AppointmentDate) 
                .ToListAsync();
        }

      
        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();

            appointment.Status = status;

            if (status == "Rejected" && !string.IsNullOrEmpty(RejectedReason))
            {
                appointment.RejectedReason = RejectedReason;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
