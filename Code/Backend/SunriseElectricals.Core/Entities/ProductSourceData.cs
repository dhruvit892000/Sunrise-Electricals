using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.Entities
{
    public class ProductSourceData
    {
        public long ProductSourceDataId { get; set; }

        public int ProductSourceId { get; set; }

        public string RawJson { get; set; } = string.Empty;

        public DateTime FetchedAt { get; set; }
    }
}
