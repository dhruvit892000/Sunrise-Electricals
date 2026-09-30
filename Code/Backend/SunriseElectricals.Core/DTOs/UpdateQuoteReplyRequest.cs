namespace SunriseElectricals.Core.DTOs
{
    public class UpdateQuoteReplyRequest
    {
        public string? ReplyMessage { get; set; }

        public string RepliedBy { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal GrandTotal { get; set; }

        public DateTime? ValidUntil { get; set; }

        public List<UpdateQuoteReplyItemRequest> Items { get; set; } = new();
    }

    public class UpdateQuoteReplyItemRequest
    {
        public int? QuoteEnquiryItemId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? TaxPercent { get; set; }

        public decimal LineTotal { get; set; }
    }
}
