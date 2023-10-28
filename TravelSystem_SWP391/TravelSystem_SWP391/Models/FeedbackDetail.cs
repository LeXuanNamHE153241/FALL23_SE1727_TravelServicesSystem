using System;
using System.Collections.Generic;

namespace TravelSystem_SWP391.Models
{
    public partial class FeedbackDetail
    {
        public virtual Feedback? Feedback { get; set; }
        public virtual User? User { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual Tour? Tour { get; set; }
    }
}
