using Dapper;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnectionProvider _provider;

        public CategoryRepository(IDbConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            const string sql = """
            SELECT
                CategoryId,
                Name,
                Slug,
                Description,
                IsActive,
                DisplayOrder,
                CreatedAt,
                UpdatedAt
            FROM Categories
            WHERE IsActive = 1
            ORDER BY DisplayOrder, Name;
            """;

            return await _provider.Connection.QueryAsync<Category>(sql);
        }
    }
}
