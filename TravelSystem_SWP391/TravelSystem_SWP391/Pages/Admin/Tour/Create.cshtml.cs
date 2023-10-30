using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Pages.Admin.Tour
{
    public class CreateModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public CreateModel(TravelSystem_SWP391.Models.traveltestContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name");
        ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Name");
        ViewData["StaffId"] = new SelectList(_context.staff, "Id", "EmailUserNavigation.Name");
        ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Tour Tour { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Tours == null || Tour == null)
            {
                return Page();
            }

            _context.Tours.Add(Tour);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
