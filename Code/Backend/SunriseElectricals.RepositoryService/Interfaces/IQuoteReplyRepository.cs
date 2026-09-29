using SunriseElectricals.Core.DTOs;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface IQuoteReplyRepository
    {
        Task<QuoteReplyResponse> CreateAsync(CreateQuoteReplyRequest request);

        Task<QuoteReplyResponse?> GetByIdAsync(int id);
    }
}
