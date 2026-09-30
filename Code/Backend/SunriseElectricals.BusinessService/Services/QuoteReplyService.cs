using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.BusinessService.Services
{
    public class QuoteReplyService : IQuoteReplyService
    {
        private readonly IQuoteReplyRepository _repository;

        public QuoteReplyService(IQuoteReplyRepository repository)
        {
            _repository = repository;
        }

        public Task<QuoteReplyResponse> CreateAsync(CreateQuoteReplyRequest request)
        {
            return _repository.CreateAsync(request);
        }

        public Task<QuoteReplyResponse?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<QuoteReplyResponse?> UpdateAsync(
            int id,
            UpdateQuoteReplyRequest request)
        {
            return _repository.UpdateAsync(id, request);
        }
    }
}
