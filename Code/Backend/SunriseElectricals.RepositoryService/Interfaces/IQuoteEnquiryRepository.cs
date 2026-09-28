using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface IQuoteEnquiryRepository
    {
        Task<QuoteEnquiry> CreateAsync(CreateQuoteEnquiryRequest request);

        Task<QuoteEnquiryResponse?> GetByIdAsync(int id);
    }
}