using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.Core.Entities
{
    public class QuoteReply
    {
        public int QuoteReplyId { get; set; }

        public int QuoteEnquiryId { get; set; }

        public string ReplyMessage { get; set; } = string.Empty;

        public string? RepliedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
