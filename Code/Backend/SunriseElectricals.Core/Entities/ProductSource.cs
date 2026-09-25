using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.Entities
{
    public class ProductSource
    {
        public int ProductSourceId { get; set; }

        public int ProductId { get; set; }

        public string SourceType { get; set; } = string.Empty;

        public string? SourceUrl { get; set; }

        public string? ExternalProductId { get; set; }

        public string? ExternalProductCode { get; set; }

        public DateTime? LastFetchedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public string SyncStatus { get; set; } = string.Empty;

        public string? SyncMessage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
