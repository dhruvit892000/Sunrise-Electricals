using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface IProductSpecificationRepository
    {
        Task<IEnumerable<ProductSpecification>> GetByProductIdAsync(int productId);
        Task<ProductSpecification> CreateAsync(CreateProductSpecificationRequest request);
    }
}
