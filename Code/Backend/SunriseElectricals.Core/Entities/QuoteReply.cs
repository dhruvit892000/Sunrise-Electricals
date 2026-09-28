namespace SunriseElectricals.Core.Entities
{
    public class QuoteReply
    {
        public int QuoteReplyId { get; set; }

        public int QuoteEnquiryId { get; set; }

        public string? ReplyMessage { get; set; }

        public string RepliedBy { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal GrandTotal { get; set; }

        public DateTime? ValidUntil { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
