using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteEnquiriesController : ControllerBase
    {
        private readonly IQuoteEnquiryService _quoteEnquiryService;

        public QuoteEnquiriesController(IQuoteEnquiryService quoteEnquiryService)
        {
            _quoteEnquiryService = quoteEnquiryService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuoteEnquiryResponse>> GetById(int id)
        {
            var enquiry = await _quoteEnquiryService.GetByIdAsync(id);

            if (enquiry is null)
            {
                return NotFound();
            }

            return Ok(enquiry);
        }

        [HttpPost]
        public async Task<ActionResult<QuoteEnquiry>> Create(
            CreateQuoteEnquiryRequest request)
        {
            var enquiry = await _quoteEnquiryService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = enquiry.QuoteEnquiryId },
                enquiry);
        }
    }
}