using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IProductSpecificationService
    {
        Task<IEnumerable<ProductSpecification>> GetByProductIdAsync(int productId);
        Task<ProductSpecification> CreateAsync(CreateProductSpecificationRequest request);
    }
}
