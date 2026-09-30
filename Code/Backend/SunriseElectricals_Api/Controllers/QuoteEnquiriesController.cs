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

        [HttpGet]
        public async Task<ActionResult<QuoteEnquiryListResponse>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Page must be at least 1 and pageSize must be between 1 and 100.");
            }

            var enquiries = await _quoteEnquiryService.GetAllAsync(
                search,
                status,
                page,
                pageSize);

            return Ok(enquiries);
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

        [HttpPut("{id:int}/status")]
        public async Task<ActionResult<QuoteEnquiryResponse>> UpdateStatus(
            int id,
            UpdateQuoteEnquiryStatusRequest request)
        {
            try
            {
                var enquiry = await _quoteEnquiryService.UpdateStatusAsync(id, request.Status);

                if (enquiry is null)
                {
                    return NotFound();
                }

                return Ok(enquiry);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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