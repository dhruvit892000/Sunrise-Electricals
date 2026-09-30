using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IQuoteEnquiryService
    {
        Task<QuoteEnquiry> CreateAsync(CreateQuoteEnquiryRequest request);

        Task<QuoteEnquiryResponse?> GetByIdAsync(int id);

        Task<QuoteEnquiryListResponse> GetAllAsync(string? search, string? status, int page, int pageSize);

        Task<QuoteEnquiryResponse?> UpdateStatusAsync(int id, string status);
    }
}