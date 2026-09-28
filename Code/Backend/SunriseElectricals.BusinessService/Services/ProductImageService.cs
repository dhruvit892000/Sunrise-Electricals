using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.BusinessService.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository repository;

        public ProductImageService(IProductImageRepository repository)
        {
            this.repository = repository;
        }

        public Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
            => repository.GetByProductIdAsync(productId);

        public Task<ProductImage> CreateAsync(int productId, CreateProductImageRequest request)
            => repository.CreateAsync(productId, request);
    }
}
