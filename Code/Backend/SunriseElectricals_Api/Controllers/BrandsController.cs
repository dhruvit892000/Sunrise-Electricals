using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
        {
            var brands = await _brandService.GetAllAsync();

            return Ok(brands);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Brand>> GetById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);

            if (brand is null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Brand>> GetBySlug(string slug)
        {
            var brand = await _brandService.GetBySlugAsync(slug);

            if (brand is null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> Create(CreateBrandRequest request)
        {
            var brand = await _brandService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = brand.BrandId },
                brand);
        }
    }
}
