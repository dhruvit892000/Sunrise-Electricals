using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryservice;

        public CategoriesController(ICategoryService categoryservice)
        {
            _categoryservice = categoryservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _categoryservice.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _categoryservice.GetByIdAsync(id);

            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<Category>> GetBySlug(string slug)
        {
            var category = await _categoryservice.GetBySlugAsync(slug);

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] CreateCategoryRequest request)
        {
            var category = await _categoryservice.CreateAsync(request);

            return CreatedAtAction(nameof(GetById),
                new
                {
                    id = category.CategoryId
                },category);
        }
    }
}
