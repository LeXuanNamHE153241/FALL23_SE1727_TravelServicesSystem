using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Tour
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? CreateDate { get; set; }
        public int? VehicleId { get; set; }
        public int? HotelId { get; set; }
        public string? AirPlane { get; set; }
        public decimal? Rating { get; set; }
        public string? Duration { get; set; }
        public bool? Status { get; set; }
        public int? StaffId { get; set; }
        public int? RestaurantId { get; set; }
        public string? Itinerary { get; set; }
        public string? Inclusions { get; set; }
        public string? Exclusions { get; set; }
        public int? GroupSize { get; set; }
        public string? Guide { get; set; }

        public virtual Hotel? Hotel { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual staff? Staff { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
