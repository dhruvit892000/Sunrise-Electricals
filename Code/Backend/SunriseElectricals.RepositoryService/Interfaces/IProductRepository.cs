using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product?> GetBySlugAsync(string slug);

        Task<Product> CreateAsync(CreateProductRequest request);
    }
}
