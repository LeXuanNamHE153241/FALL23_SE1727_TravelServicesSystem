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
        public string RoleStaff { get; set; } = null!;
        public int Rate { get; set; }
        public string? Description { get; set; }
        public string? EmailUser { get; set; }

        public virtual User? EmailUserNavigation { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
