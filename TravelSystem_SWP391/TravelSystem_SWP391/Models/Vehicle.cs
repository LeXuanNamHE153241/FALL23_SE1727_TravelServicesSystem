using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Bookings = new HashSet<Booking>();
            Schedules = new HashSet<Schedule>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double Price { get; set; }
        public string Image { get; set; } = null!;
        public int Rate { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
