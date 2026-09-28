namespace SunriseElectricals.Core.DTOs
{
    public class CreateQuoteEnquiryRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        public string? CompanyName { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Location { get; set; }

        public string? Message { get; set; }

        public List<CreateQuoteEnquiryItemRequest> Items { get; set; } = new();
    }

    public class CreateQuoteEnquiryItemRequest
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }

        public string? CustomerRequirement { get; set; }
    }
}
