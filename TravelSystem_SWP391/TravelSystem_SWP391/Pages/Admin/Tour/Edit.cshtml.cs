using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Pages.Admin.Tour
{
    public class EditModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public EditModel(TravelSystem_SWP391.Models.traveltestContext context)
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

            var tour =  await _context.Tours.FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }
            Tour = tour;
           ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Id");
           ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
           ViewData["StaffId"] = new SelectList(_context.staff, "Id", "Id");
           ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Tour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(Tour.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TourExists(int id)
        {
          return (_context.Tours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
