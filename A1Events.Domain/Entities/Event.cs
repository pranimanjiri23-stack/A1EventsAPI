using A1Events.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1Events.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Location { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public EventStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties

        public ICollection<VendorApplication> VendorApplications { get; set; }
            = new List<VendorApplication>();
    }
}
