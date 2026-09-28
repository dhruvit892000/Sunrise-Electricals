using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId);
        Task<ProductImage> CreateAsync(int productId, CreateProductImageRequest request);
    }
}
