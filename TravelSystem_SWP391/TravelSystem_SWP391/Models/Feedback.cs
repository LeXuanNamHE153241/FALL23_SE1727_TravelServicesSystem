using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class Feedback
    {
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

        public virtual User EmailNavigation { get; set; } = null!;
    }
}
