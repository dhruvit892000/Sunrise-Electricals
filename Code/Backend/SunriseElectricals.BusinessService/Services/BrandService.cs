using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.BusinessService.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<Brand?> GetBySlugAsync(string slug)
        {
            return await _brandRepository.GetBySlugAsync(slug);
        }

        public async Task<Brand> CreateAsync(CreateBrandRequest request)
        {
            return await _brandRepository.CreateAsync(request);
        }
    }
}
