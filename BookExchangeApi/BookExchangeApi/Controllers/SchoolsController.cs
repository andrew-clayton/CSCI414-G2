using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolsRepository _repository;

        public SchoolsController(SchoolsRepository repository)
        {
            _repository = repository;
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> CreateSchool([FromBody] School school)
        {
            try
            {
                await _repository.CreateSchoolAsync(school);
                return CreatedAtAction(nameof(GetSchoolById), new { id = school.SchoolId }, school);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Schools/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSchoolById(int id)
        {
            try
            {
                var school = await _repository.GetSchoolByIdAsync(id);
                if (school == null)
                    return NotFound();

                return Ok(school);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Schools/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] School school)
        {
            if (id != school.SchoolId)
                return BadRequest("School ID mismatch");

            try
            {
                var existingSchool = await _repository.GetSchoolByIdAsync(id);
                if (existingSchool == null)
                    return NotFound();

                await _repository.UpdateSchoolAsync(school);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Schools/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            try
            {
                var existingSchool = await _repository.GetSchoolByIdAsync(id);
                if (existingSchool == null)
                    return NotFound();

                await _repository.DeleteSchoolAsync(id);
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
