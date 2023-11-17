using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Information
    {
        public int Id { get; set; }
        public string? InformationName { get; set; }
        public string? InformationDescription { get; set; }
        public int? InformationPriority { get; set; }
        public int? TourId { get; set; }
        public string? EmailUser { get; set; }

        public virtual User? EmailUserNavigation { get; set; }
        public virtual Tour? Tour { get; set; }
    }
}
