using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.Entities
{
    public class QuoteEnquiry
    {
        public int QuoteEnquiryId { get; set; }

        public string QuoteNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string? CompanyName { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Location { get; set; }

        public string? Message { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
