using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync();

        Task<Brand?> GetByIdAsync(int id);

        Task<Brand?> GetBySlugAsync(string slug);

        Task<Brand> CreateAsync(CreateBrandRequest request);
    }
}
