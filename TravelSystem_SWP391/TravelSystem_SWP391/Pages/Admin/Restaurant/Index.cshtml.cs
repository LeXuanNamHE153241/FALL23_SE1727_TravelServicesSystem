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
    public class IndexModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public IndexModel(TravelSystem_SWP391.Models.traveltestContext context)
        {
            _context = context;
        }

        public IList<Models.Restaurant> Restaurant { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Restaurants != null)
            {
                Restaurant = await _context.Restaurants.ToListAsync();
            }
        }
    }
}
