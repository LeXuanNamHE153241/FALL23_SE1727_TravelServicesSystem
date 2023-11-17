using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Location
    {
        public Location()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string? LocationCode { get; set; }
        public string? LocationName { get; set; }
        public string? LocationImage { get; set; }
        public string? EmailUser { get; set; }
        public int? CountryId { get; set; }

        public virtual Country? Country { get; set; }
        public virtual User? EmailUserNavigation { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
