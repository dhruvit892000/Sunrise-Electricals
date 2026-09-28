namespace SunriseElectricals.Core.DTOs
{
    public class CreateProductRequest
    {
        public int CategoryId { get; set; }

        public int? BrandId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? PartNumber { get; set; }

        public string? ModelNumber { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
    }
}