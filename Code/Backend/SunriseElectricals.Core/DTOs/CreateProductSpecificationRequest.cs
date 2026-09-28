namespace SunriseElectricals.Core.DTOs
{
    public class CreateProductSpecificationRequest
    {
        public int ProductId { get; set; }
        public string SpecificationName { get; set; } = string.Empty;
        public string SpecificationValue { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
