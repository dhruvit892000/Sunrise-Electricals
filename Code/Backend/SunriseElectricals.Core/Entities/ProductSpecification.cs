using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.Entities
{
    public class ProductSpecification
    {
        public int ProductSpecificationId { get; set; }

        public int ProductId { get; set; }

        public string SpecificationName { get; set; } = string.Empty;

        public string SpecificationValue { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
