namespace SunriseElectricals.Core.DTOs
{
    public class QuoteEnquiryResponse
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

        public DateTime? UpdatedAt { get; set; }

        public List<QuoteEnquiryItemResponse> Items { get; set; } = new();
    }

    public class QuoteEnquiryItemResponse
    {
        public int QuoteEnquiryItemId { get; set; }

        public int QuoteEnquiryId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }

        public string? CustomerRequirement { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}