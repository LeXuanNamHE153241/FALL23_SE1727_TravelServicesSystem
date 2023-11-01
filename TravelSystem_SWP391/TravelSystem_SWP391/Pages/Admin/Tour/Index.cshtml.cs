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
    public class IndexModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;

        public IndexModel(TravelSystem_SWP391.Models.traveltestContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IList<Models.Tour> Tour { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                SearchTerm = SearchTerm.ToLower();
                Tour = await _context.Tours
                    .Include(t => t.Hotel)
                    .Include(t => t.Restaurant)
                    .Include(t => t.Staff)
                    .Include(t => t.Vehicle)
                    .Where(t => t.Name!.ToLower().Contains(SearchTerm))
                    .ToListAsync();
            }
            else
            {
                if (_context.Tours != null)
                {
                    Tour = await _context.Tours
                        .Include(t => t.Hotel)
                        .Include(t => t.Restaurant)
                        .Include(t => t.Staff)
                        .Include(t => t.Vehicle)
                        .ToListAsync();
                }
            }

        }
    }
}
