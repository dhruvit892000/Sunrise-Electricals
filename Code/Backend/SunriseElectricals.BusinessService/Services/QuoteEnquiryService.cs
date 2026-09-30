using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.BusinessService.Services
{
    public class QuoteEnquiryService : IQuoteEnquiryService
    {
        private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
        {
            "New",
            "Under Review",
            "Quoted",
            "Customer Responded",
            "Accepted",
            "Rejected",
            "Closed"
        };

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

        public async Task<QuoteEnquiryResponse?> UpdateStatusAsync(int id, string status)
        {
            var normalizedStatus = status?.Trim();

            if (string.IsNullOrWhiteSpace(normalizedStatus) || !AllowedStatuses.Contains(normalizedStatus))
            {
                throw new ArgumentException(
                    "Status must be one of: New, Under Review, Quoted, Customer Responded, Accepted, Rejected, Closed.");
            }

            normalizedStatus = GetCanonicalStatus(normalizedStatus);

            var enquiry = await _repository.GetByIdAsync(id);

            if (enquiry is null)
            {
                return null;
            }

            var currentStatus = enquiry.Status.Trim();

            if (!IsValidTransition(currentStatus, normalizedStatus))
            {
                throw new InvalidOperationException(
                    $"Invalid status transition from '{currentStatus}' to '{normalizedStatus}'.");
            }

            return await _repository.UpdateStatusAsync(id, normalizedStatus);
        }

        private static bool IsValidTransition(string currentStatus, string newStatus)
        {
            if (string.Equals(currentStatus, newStatus, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return currentStatus switch
            {
                "New" => newStatus is "Under Review" or "Closed",
                "Under Review" => newStatus is "Quoted" or "Closed",
                "Quoted" => newStatus is "Customer Responded" or "Closed",
                "Customer Responded" => newStatus is "Accepted" or "Rejected" or "Closed",
                "Accepted" => newStatus == "Closed",
                "Rejected" => newStatus == "Closed",
                "Closed" => false,
                _ => false
            };
        }

        private static string GetCanonicalStatus(string status)
        {
            if (string.Equals(status, "New", StringComparison.OrdinalIgnoreCase))
            {
                return "New";
            }

            if (string.Equals(status, "Under Review", StringComparison.OrdinalIgnoreCase))
            {
                return "Under Review";
            }

            if (string.Equals(status, "Quoted", StringComparison.OrdinalIgnoreCase))
            {
                return "Quoted";
            }

            if (string.Equals(status, "Customer Responded", StringComparison.OrdinalIgnoreCase))
            {
                return "Customer Responded";
            }

            if (string.Equals(status, "Accepted", StringComparison.OrdinalIgnoreCase))
            {
                return "Accepted";
            }

            if (string.Equals(status, "Rejected", StringComparison.OrdinalIgnoreCase))
            {
                return "Rejected";
            }

            if (string.Equals(status, "Closed", StringComparison.OrdinalIgnoreCase))
            {
                return "Closed";
            }

            return status;
        }
    }
}