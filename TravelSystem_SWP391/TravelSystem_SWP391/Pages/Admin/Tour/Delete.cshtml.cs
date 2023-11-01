using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Pages.Admin.Tour
{
    public class DeleteModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public DeleteModel(TravelSystem_SWP391.Models.traveltestContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Tour Tour { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FirstOrDefaultAsync(m => m.Id == id);

            if (tour == null)
            {
                return NotFound();
            }
            else 
            {
                Tour = tour;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }
            var tour = await _context.Tours.FindAsync(id);

            if (tour != null)
            {
                Tour = tour;
                _context.Tours.Remove(Tour);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
