using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedUnity.Pages
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
        public int Id { get; set; }

        [BindProperty]
        public string Action { get; set; }

        public async Task OnGetAsync()
        {
            Appointments = await _context.Appointments
                .OrderBy(a => a.AppointmentDate).ToListAsync();
        }

    }
}
