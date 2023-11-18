using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public string? ScheduleName { get; set; }
        public string? ScheduleDescription { get; set; }
        public string? ScheduleImage { get; set; }
        public int? SchedulePriority { get; set; }
        public int? LocationId { get; set; }
        public int? TourId { get; set; }
        public int? HotelId { get; set; }
        public int? RestaurantId { get; set; }
        public int? VehicleId { get; set; }
        public string? EmailUser { get; set; }

        public virtual User? EmailUserNavigation { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual Location? Location { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual Tour? Tour { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

    }
}
