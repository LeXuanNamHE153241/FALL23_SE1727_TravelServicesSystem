using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Pages.Admin.Hotel
{
    public class DetailsModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public DetailsModel(TravelSystem_SWP391.Models.traveltestContext context)
        {
            _context = context;
        }

      public Models.Hotel Hotel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            else 
            {
                Hotel = hotel;
            }
            return Page();
        }
    }
}
