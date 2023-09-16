using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class staff
    {
        public staff()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleStaff { get; set; } = null!;
        public int Rate { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
