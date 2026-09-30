using Microsoft.AspNetCore.Mvc;
using SunriseElectricals.BusinessService.Interfaces;
using SunriseElectricals.Core.DTOs;

namespace SunriseElectricals_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteRepliesController : ControllerBase
    {
        private readonly IQuoteReplyService _quoteReplyService;

        public QuoteRepliesController(IQuoteReplyService quoteReplyService)
        {
            _quoteReplyService = quoteReplyService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuoteReplyResponse>> GetById(int id)
        {
            var reply = await _quoteReplyService.GetByIdAsync(id);

            if (reply is null)
            {
                return NotFound();
            }

            return Ok(reply);
        }

        [HttpGet("enquiry/{quoteEnquiryId:int}")]
        public async Task<ActionResult<QuoteReplyResponse>> GetByEnquiryId(int quoteEnquiryId)
        {
            var reply = await _quoteReplyService.GetByEnquiryIdAsync(quoteEnquiryId);

            if (reply is null)
            {
                return NotFound();
            }

            return Ok(reply);
        }

        [HttpPost]
        public async Task<ActionResult<QuoteReplyResponse>> Create(
            CreateQuoteReplyRequest request)
        {
            var reply = await _quoteReplyService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = reply.QuoteReplyId },
                reply);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<QuoteReplyResponse>> Update(
            int id,
            UpdateQuoteReplyRequest request)
        {
            var reply = await _quoteReplyService.UpdateAsync(id, request);

            if (reply is null)
            {
                return NotFound();
            }

            return Ok(reply);
        }
    }
}
