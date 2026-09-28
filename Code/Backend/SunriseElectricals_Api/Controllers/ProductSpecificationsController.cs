using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/products/{productId:int}/specifications")]
    [ApiController]
    public class ProductSpecificationsController : ControllerBase
    {
        private readonly IProductSpecificationService _productSpecificationService;

        public ProductSpecificationsController(
            IProductSpecificationService productSpecificationService)
        {
            _productSpecificationService = productSpecificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var specifications =
                await _productSpecificationService.GetByProductIdAsync(productId);

            return Ok(specifications);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            int productId,
            CreateProductSpecificationRequest request)
        {
            request.ProductId = productId;

            var specification =
                await _productSpecificationService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetByProductId),
                new { productId = specification.ProductId },
                specification);
        }
    }
}
