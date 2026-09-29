using SunriseElectricals.Core.DTOs;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IQuoteReplyService
    {
        Task<QuoteReplyResponse> CreateAsync(CreateQuoteReplyRequest request);

        Task<QuoteReplyResponse?> GetByIdAsync(int id);
    }
}
