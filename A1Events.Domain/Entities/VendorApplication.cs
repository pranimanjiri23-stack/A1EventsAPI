using A1Events.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1Events.Domain.Entities
{
    public class VendorApplication
    {
        public int Id { get; set; }

        public int VendorId { get; set; }

        public int EventId { get; set; }

        public VendorApplicationStatus Status { get; set; }

        public string? AdminRemarks { get; set; }

        public DateTime AppliedAt { get; set; }

        // Navigation properties

        public Vendor Vendor { get; set; } = null!;

        public Event Event { get; set; } = null!;
    }
}
