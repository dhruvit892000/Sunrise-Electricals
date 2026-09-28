namespace SunriseElectricals.Core.Entities
{
    public class QuoteReplyItem
    {
        public int QuoteReplyItemId { get; set; }

        public int QuoteReplyId { get; set; }

        public int? QuoteEnquiryItemId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? TaxPercent { get; set; }

        public decimal LineTotal { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
