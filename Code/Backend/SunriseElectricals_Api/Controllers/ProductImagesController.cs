using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/products/{productId:int}/images")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetByProductId(int productId)
        {
            var images = await _productImageService.GetByProductIdAsync(productId);
            return Ok(images);
        }

        [HttpPost]
        public async Task<ActionResult<ProductImage>> Create(
            int productId,
            [FromBody] CreateProductImageRequest request)
        {
            var image = await _productImageService.CreateAsync(productId, request);

            return CreatedAtAction(
                nameof(GetByProductId),
                new { productId },
                image);
        }
    }
}
