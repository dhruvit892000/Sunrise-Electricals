namespace SunriseElectricals.Core.DTOs
{
    public class QuoteEnquiryListResponse
    {
        public List<QuoteEnquiryListItemResponse> Items { get; set; } = new();

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages =>
            PageSize <= 0
                ? 0
                : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public class QuoteEnquiryListItemResponse
    {
        public int QuoteEnquiryId { get; set; }

        public string QuoteNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string? CompanyName { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Location { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
