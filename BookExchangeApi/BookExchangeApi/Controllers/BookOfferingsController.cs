using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookOfferingsController : ControllerBase
    {
        private readonly BookOfferingsRepository _repository;

        public BookOfferingsController(BookOfferingsRepository repository)
        {
            _repository = repository;
        }

        // POST: api/BookOfferings
        [HttpPost]
        public async Task<IActionResult> CreateBookOffering([FromBody] BookOffering offering)
        {
            try
            {
                await _repository.CreateBookOfferingAsync(offering);
                return CreatedAtAction(nameof(GetBookOfferingById), new { id = offering.BookOfferingId }, offering);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/BookOfferings/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookOfferingById(int id)
        {
            try
            {
                var offering = await _repository.GetBookOfferingByIdAsync(id);
                if (offering == null)
                    return NotFound();

                return Ok(offering);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/BookOfferings/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookOffering(int id, [FromBody] BookOffering offering)
        {
            if (id != offering.BookOfferingId)
                return BadRequest("Book Offering ID mismatch");

            try
            {
                var existingOffering = await _repository.GetBookOfferingByIdAsync(id);
                if (existingOffering == null)
                    return NotFound();

                await _repository.UpdateBookOfferingAsync(offering);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/BookOfferings/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBookOffering(int id)
        {
            try
            {
                var existingOffering = await _repository.GetBookOfferingByIdAsync(id);
                if (existingOffering == null)
                    return NotFound();

                await _repository.DeleteBookOfferingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
