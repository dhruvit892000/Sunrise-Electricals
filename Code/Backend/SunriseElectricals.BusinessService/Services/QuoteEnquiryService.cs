using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.BusinessService.Services
{
    public class QuoteEnquiryService : IQuoteEnquiryService
    {
        private readonly IQuoteEnquiryRepository _repository;

        public QuoteEnquiryService(IQuoteEnquiryRepository repository)
        {
            _repository = repository;
        }

        public Task<QuoteEnquiry> CreateAsync(CreateQuoteEnquiryRequest request)
        {
            return _repository.CreateAsync(request);
        }

        public Task<QuoteEnquiryListResponse> GetAllAsync(string? search, string? status, int page, int pageSize)
        {
            return _repository.GetAllAsync(search, status, page, pageSize);
        }

        public Task<QuoteEnquiryResponse?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
    }
}