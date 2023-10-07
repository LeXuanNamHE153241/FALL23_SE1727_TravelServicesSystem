using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            staff = new HashSet<staff>();
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? RoleId { get; set; }
        public string? Action { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }

        public virtual Feedback Feedback { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
