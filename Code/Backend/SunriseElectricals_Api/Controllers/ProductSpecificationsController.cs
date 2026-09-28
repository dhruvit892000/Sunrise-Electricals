using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationsController : ControllerBase
    {
        private readonly IProductSpecificationService _productSpecificationService;

        public ProductSpecificationsController(
            IProductSpecificationService productSpecificationService)
        {
            _productSpecificationService = productSpecificationService;
        }

        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var specifications =
                await _productSpecificationService.GetByProductIdAsync(productId);

            return Ok(specifications);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductSpecificationRequest request)
        {
            var specification =
                await _productSpecificationService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetByProductId),
                new { productId = specification.ProductId },
                specification);
        }
    }
}
