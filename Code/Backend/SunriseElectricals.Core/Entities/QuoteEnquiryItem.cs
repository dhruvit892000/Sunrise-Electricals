using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.Entities
{
    public class QuoteEnquiryItem
    {
        public int QuoteEnquiryItemId { get; set; }

        public int QuoteEnquiryId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public string? Unit { get; set; }

        public string? CustomerRequirement { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
