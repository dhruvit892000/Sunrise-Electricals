using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.DTOs
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
    }
}
