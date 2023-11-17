using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Country
    {
        public Country()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? CountryImage { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
