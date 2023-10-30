using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Pages.Admin.Vehicle
{
    public class CreateModel : PageModel
    {
        private readonly TravelSystem_SWP391.Models.traveltestContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(TravelSystem_SWP391.Models.traveltestContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Vehicle Vehicle { get; set; } = default!;
        [BindProperty]
        public IFormFile VehicleImage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Vehicles == null || Vehicle == null)
            {
                return Page();
            }
            if (VehicleImage != null)
            {
                // Lấy đường dẫn thư mục wwwroot/images
                var webRootPath = _env.WebRootPath;
                var imagePath = Path.Combine(webRootPath, "uploads");

                // Tạo tên tệp hình ảnh duy nhất
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + VehicleImage.FileName;

                // Tạo đường dẫn đầy đủ cho tệp hình ảnh
                var filePath = Path.Combine(imagePath, uniqueFileName);

                // Lưu trữ tệp hình ảnh vào thư mục wwwroot/images
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await VehicleImage.CopyToAsync(stream);
                }

                // Gán đường dẫn hình ảnh cho trường Hotel.Image
                Vehicle.Image = "images/" + uniqueFileName;
            }
            _context.Vehicles.Add(Vehicle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
