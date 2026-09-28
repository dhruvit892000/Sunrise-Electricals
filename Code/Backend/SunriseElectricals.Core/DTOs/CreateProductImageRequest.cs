namespace SunriseElectricals.Core.DTOs
{
    public class CreateProductImageRequest
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string? AltText { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}
