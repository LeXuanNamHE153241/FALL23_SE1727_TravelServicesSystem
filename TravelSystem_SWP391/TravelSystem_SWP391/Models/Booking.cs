using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumPeople { get; set; }
        public string Message { get; set; } = null!;
        public int? TourId { get; set; }
        public int? HotelId { get; set; }
        public int? RestaurantId { get; set; }
        public int? VehicleId { get; set; }

        public virtual User EmailNavigation { get; set; } = null!;
        public virtual Hotel? Hotel { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual Tour? Tour { get; set; }
       
    }
}
