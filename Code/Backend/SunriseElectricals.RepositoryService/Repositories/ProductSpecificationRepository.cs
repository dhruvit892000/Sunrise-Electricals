using Dapper;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class ProductSpecificationRepository : IProductSpecificationRepository
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public ProductSpecificationRepository(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<ProductSpecification>> GetByProductIdAsync(int productId)
        {
            const string sql = @"
                SELECT
                    ProductSpecificationId,
                    ProductId,
                    SpecificationName,
                    SpecificationValue,
                    DisplayOrder,
                    CreatedAt,
                    UpdatedAt
                FROM ProductSpecifications
                WHERE ProductId = @ProductId
                ORDER BY DisplayOrder, ProductSpecificationId;";

            using var connection = _connectionProvider.CreateConnection();

            return await connection.QueryAsync<ProductSpecification>(
                sql,
                new { ProductId = productId });
        }

        public async Task<ProductSpecification> CreateAsync(CreateProductSpecificationRequest request)
        {
            const string sql = @"
                INSERT INTO ProductSpecifications
                (
                    ProductId,
                    SpecificationName,
                    SpecificationValue,
                    DisplayOrder,
                    CreatedAt
                )
                OUTPUT
                    INSERTED.ProductSpecificationId,
                    INSERTED.ProductId,
                    INSERTED.SpecificationName,
                    INSERTED.SpecificationValue,
                    INSERTED.DisplayOrder,
                    INSERTED.CreatedAt,
                    INSERTED.UpdatedAt
                VALUES
                (
                    @ProductId,
                    @SpecificationName,
                    @SpecificationValue,
                    @DisplayOrder,
                    GETUTCDATE()
                );";

            using var connection = _connectionProvider.CreateConnection();

            return await connection.QuerySingleAsync<ProductSpecification>(
                sql,
                request);
        }
    }
}
