using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.BusinessService.Services
{
    public class ProductSpecificationService : IProductSpecificationService
    {
        private readonly IProductSpecificationRepository _repository;

        public ProductSpecificationService(IProductSpecificationRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ProductSpecification>> GetByProductIdAsync(int productId)
        {
            return _repository.GetByProductIdAsync(productId);
        }

        public Task<ProductSpecification> CreateAsync(CreateProductSpecificationRequest request)
        {
            return _repository.CreateAsync(request);
        }
    }
}
