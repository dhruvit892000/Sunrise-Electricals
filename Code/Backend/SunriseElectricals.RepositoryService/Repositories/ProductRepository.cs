using Dapper;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionProvider _provider;

        public ProductRepository(IDbConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            const string sql = """
            SELECT
                ProductId,
                CategoryId,
                BrandId,
                Name,
                Slug,
                PartNumber,
                ModelNumber,
                ShortDescription,
                Description,
                IsFeatured,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt,
                LastSyncedAt
            FROM Products
            WHERE IsActive = 1
            ORDER BY DisplayOrder, Name;
            """;

            return await _provider.Connection.QueryAsync<Product>(sql);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            const string sql = """
            SELECT
                ProductId,
                CategoryId,
                BrandId,
                Name,
                Slug,
                PartNumber,
                ModelNumber,
                ShortDescription,
                Description,
                IsFeatured,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt,
                LastSyncedAt
            FROM Products
            WHERE ProductId = @Id;
            """;

            return await _provider.Connection.QueryFirstOrDefaultAsync<Product>(
                sql,
                new { Id = id });
        }

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            const string sql = """
            SELECT
                ProductId,
                CategoryId,
                BrandId,
                Name,
                Slug,
                PartNumber,
                ModelNumber,
                ShortDescription,
                Description,
                IsFeatured,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt,
                LastSyncedAt
            FROM Products
            WHERE Slug = @Slug
              AND IsActive = 1;
            """;

            return await _provider.Connection.QueryFirstOrDefaultAsync<Product>(
                sql,
                new { Slug = slug });
        }

        public async Task<Product> CreateAsync(CreateProductRequest request)
        {
            const string sql = """
            INSERT INTO Products
            (
                CategoryId,
                BrandId,
                Name,
                Slug,
                PartNumber,
                ModelNumber,
                ShortDescription,
                Description,
                IsFeatured,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt
            )
            OUTPUT
                INSERTED.ProductId,
                INSERTED.CategoryId,
                INSERTED.BrandId,
                INSERTED.Name,
                INSERTED.Slug,
                INSERTED.PartNumber,
                INSERTED.ModelNumber,
                INSERTED.ShortDescription,
                INSERTED.Description,
                INSERTED.IsFeatured,
                INSERTED.IsActive,
                INSERTED.DisplayOrder,
                INSERTED.CreatedAt,
                INSERTED.UpdatedAt,
                INSERTED.LastSyncedAt
            VALUES
            (
                @CategoryId,
                @BrandId,
                @Name,
                @Slug,
                @PartNumber,
                @ModelNumber,
                @ShortDescription,
                @Description,
                @IsFeatured,
                @IsActive,
                @DisplayOrder,
                GETUTCDATE(),
                GETUTCDATE()
            );
            """;

            return await _provider.Connection.QuerySingleAsync<Product>(
                sql,
                request);
        }
    }
}
