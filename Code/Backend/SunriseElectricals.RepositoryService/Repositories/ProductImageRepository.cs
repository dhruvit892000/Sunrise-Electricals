using Dapper;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly IDbConnectionProvider _provider;

        public ProductImageRepository(IDbConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            const string sql = """
            SELECT
                ProductImageId,
                ProductId,
                ImageUrl,
                AltText,
                IsPrimary,
                DisplayOrder,
                CreatedAt
            FROM ProductImages
            WHERE ProductId = @ProductId
            ORDER BY IsPrimary DESC, DisplayOrder, ProductImageId;
            """;

            return await _provider.Connection.QueryAsync<ProductImage>(
                sql,
                new { ProductId = productId });
        }

        public async Task<ProductImage> CreateAsync(int productId, CreateProductImageRequest request)
        {
            const string sql = """
            INSERT INTO ProductImages
            (
                ProductId,
                ImageUrl,
                AltText,
                IsPrimary,
                DisplayOrder,
                CreatedAt
            )
            OUTPUT
                INSERTED.ProductImageId,
                INSERTED.ProductId,
                INSERTED.ImageUrl,
                INSERTED.AltText,
                INSERTED.IsPrimary,
                INSERTED.DisplayOrder,
                INSERTED.CreatedAt
            VALUES
            (
                @ProductId,
                @ImageUrl,
                @AltText,
                @IsPrimary,
                @DisplayOrder,
                GETUTCDATE()
            );
            """;

            return await _provider.Connection.QuerySingleAsync<ProductImage>(
                sql,
                new
                {
                    ProductId = productId,
                    request.ImageUrl,
                    request.AltText,
                    request.IsPrimary,
                    request.DisplayOrder
                });
        }
    }
}
