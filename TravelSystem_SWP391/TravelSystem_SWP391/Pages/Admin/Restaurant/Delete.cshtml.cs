using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Pages.Admin.Restaurant
{
    public class DeleteModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public DeleteModel(TravelSystem_SWP391.Models.traveltestContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Restaurant Restaurant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(m => m.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            else 
            {
                Restaurant = restaurant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                Restaurant = restaurant;
                _context.Restaurants.Remove(Restaurant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
