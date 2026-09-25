using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.RepositoryService.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetBySlugAsync(string slug);
        Task<Category> CreateAsync(CreateCategoryRequest request);
    }
}
