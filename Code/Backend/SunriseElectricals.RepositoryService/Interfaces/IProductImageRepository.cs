using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId);
        Task<ProductImage> CreateAsync(int productId, CreateProductImageRequest request);
    }
}
