using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedUnity.Pages
{
    public class WellnessUpdatesModel : PageModel
    {
        private readonly AppDbContext _context;

        public WellnessUpdatesModel(AppDbContext context)
        {
            _context = context;
        }


        public IList<WellnessUpdate> AllUpdates { get; set; } = new List<WellnessUpdate>();

        public async Task OnGetAsync()
        {
   
            AllUpdates = await _context.WellnessUpdates
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
        }
    }
}
