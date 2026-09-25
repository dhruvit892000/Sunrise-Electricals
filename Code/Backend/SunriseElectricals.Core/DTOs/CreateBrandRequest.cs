using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.DTOs
{
    public class CreateBrandRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? WebsiteUrl { get; set; }

        public string? ManufacturerCode { get; set; }

        public string? Description { get; set; }

        public string? LogoUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
    }
}
