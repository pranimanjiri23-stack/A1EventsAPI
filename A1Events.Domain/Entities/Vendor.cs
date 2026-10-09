using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1Events.Domain.Entities
{
    public class Vendor
    {
        public int Id { get; set; }

        public string BusinessName { get; set; } = string.Empty;

        public string ContactPerson { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? InstagramUrl { get; set; }

        public int CategoryId { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties

        public Category Category { get; set; } = null!;

        public ICollection<VendorApplication> Applications { get; set; }
            = new List<VendorApplication>();
    }
}
