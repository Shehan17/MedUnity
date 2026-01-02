using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedUnity.Pages
{
    public class WellnessUpdatesModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Content { get; set; }
        [BindProperty]
        public string ImagePath { get; set; }
        [BindProperty]
        public string Source { get; set; }
        public void OnGet()
        {
        }
    }
}
