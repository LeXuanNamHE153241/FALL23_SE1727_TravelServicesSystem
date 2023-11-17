using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Bookings = new HashSet<Booking>();
            Schedules = new HashSet<Schedule>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public double? Rating { get; set; }
        public string? Image { get; set; }
        public string? Review { get; set; }
        public string? RoomTypes { get; set; }
        public string? Amenities { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
