using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceApp.Models;

namespace ServiceApp.Pages
{
    public class IndexModel : PageModel
    {

        private PRN221_Spr22Context _context;
        public IndexModel(PRN221_Spr22Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            _context.Rooms.ToList();
        }
    }
}