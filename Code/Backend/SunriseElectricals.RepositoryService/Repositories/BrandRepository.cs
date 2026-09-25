using Dapper;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IDbConnectionProvider _provider;

        public BrandRepository(IDbConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            const string sql = """
            SELECT
                BrandId,
                Name,
                Slug,
                WebsiteUrl,
                ManufacturerCode,
                Description,
                LogoUrl,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt
            FROM Brands
            WHERE IsActive = 1
            ORDER BY DisplayOrder, Name;
            """;

            return await _provider.Connection.QueryAsync<Brand>(sql);
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            const string sql = """
            SELECT
                BrandId,
                Name,
                Slug,
                WebsiteUrl,
                ManufacturerCode,
                Description,
                LogoUrl,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt
            FROM Brands
            WHERE BrandId = @Id;
            """;

            return await _provider.Connection.QueryFirstOrDefaultAsync<Brand>(
                sql,
                new { Id = id });
        }

        public async Task<Brand?> GetBySlugAsync(string slug)
        {
            const string sql = """
            SELECT
                BrandId,
                Name,
                Slug,
                WebsiteUrl,
                ManufacturerCode,
                Description,
                LogoUrl,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt
            FROM Brands
            WHERE Slug = @Slug
              AND IsActive = 1;
            """;

            return await _provider.Connection.QueryFirstOrDefaultAsync<Brand>(
                sql,
                new { Slug = slug });
        }

        public async Task<Brand> CreateAsync(CreateBrandRequest request)
        {
            const string sql = """
            INSERT INTO Brands
            (
                Name,
                Slug,
                WebsiteUrl,
                ManufacturerCode,
                Description,
                LogoUrl,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt
            )
            OUTPUT
                INSERTED.BrandId,
                INSERTED.Name,
                INSERTED.Slug,
                INSERTED.WebsiteUrl,
                INSERTED.ManufacturerCode,
                INSERTED.Description,
                INSERTED.LogoUrl,
                INSERTED.IsActive,
                INSERTED.DisplayOrder,
                INSERTED.CreatedAt,
                INSERTED.UpdatedAt
            VALUES
            (
                @Name,
                @Slug,
                @WebsiteUrl,
                @ManufacturerCode,
                @Description,
                @LogoUrl,
                @IsActive,
                @DisplayOrder,
                GETUTCDATE(),
                GETUTCDATE()
            );
            """;

            return await _provider.Connection.QuerySingleAsync<Brand>(
                sql,
                request);
        }
    }
}
