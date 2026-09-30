using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface IQuoteEnquiryRepository
    {
        Task<QuoteEnquiry> CreateAsync(CreateQuoteEnquiryRequest request);

        Task<QuoteEnquiryResponse?> GetByIdAsync(int id);

        Task<QuoteEnquiryListResponse> GetAllAsync(string? search, string? status, int page, int pageSize);
    }
}