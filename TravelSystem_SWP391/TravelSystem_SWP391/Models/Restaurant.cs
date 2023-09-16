using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Bookings = new HashSet<Booking>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Price { get; set; } = null!;
        public int Rate { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
