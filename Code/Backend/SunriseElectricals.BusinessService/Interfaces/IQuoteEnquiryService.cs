using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IQuoteEnquiryService
    {
        Task<QuoteEnquiry> CreateAsync(CreateQuoteEnquiryRequest request);

        Task<QuoteEnquiry?> GetByIdAsync(int id);
    }
}
