using MedUnity.Data;
using MedUnity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class ManageWellnessUpdateModel : PageModel
{
    private readonly AppDbContext _context;

    public ManageWellnessUpdateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public WellnessUpdate WellnessUpdate { get; set; } = new();

    [BindProperty]
    public IFormFile? ImageFile { get; set; }

    
    public IList<WellnessUpdate> AllUpdates { get; set; } = new List<WellnessUpdate>();

    
    [BindProperty]
    public int WellnessUpdateId { get; set; }

   
    public async Task OnGetAsync()
    {
        AllUpdates = await _context.WellnessUpdates
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (ImageFile != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await ImageFile.CopyToAsync(stream);

            WellnessUpdate.ImagePath = "/images/" + fileName;
        }

        _context.WellnessUpdates.Add(WellnessUpdate);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        var update = await _context.WellnessUpdates.FindAsync(WellnessUpdateId); 
        if (update == null) return NotFound();

        update.Title = WellnessUpdate.Title;
        update.Content = WellnessUpdate.Content;
        update.SourceUrl = WellnessUpdate.SourceUrl;

        if (ImageFile != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await ImageFile.CopyToAsync(stream);
            update.ImagePath = "/images/" + fileName;
        }

        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

   
    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var update = await _context.WellnessUpdates.FindAsync(WellnessUpdateId); 
        if (update != null)
        {
            _context.WellnessUpdates.Remove(update);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
