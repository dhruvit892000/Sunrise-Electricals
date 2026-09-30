using SunriseElectricals.Core.DTOs;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IQuoteReplyService
    {
        Task<QuoteReplyResponse> CreateAsync(CreateQuoteReplyRequest request);

        Task<QuoteReplyResponse?> GetByIdAsync(int id);

        Task<QuoteReplyResponse?> GetByEnquiryIdAsync(int quoteEnquiryId);

        Task<QuoteReplyResponse?> UpdateAsync(
            int id,
            UpdateQuoteReplyRequest request);
    }
}
