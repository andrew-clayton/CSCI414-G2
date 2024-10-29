using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesRepository _repository;

        public CoursesController(CoursesRepository repository)
        {
            _repository = repository;
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            try
            {
                await _repository.CreateCourseAsync(course);
                return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Courses/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _repository.GetCourseByIdAsync(id);
                if (course == null)
                    return NotFound();

                return Ok(course);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Courses/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.CourseId)
                return BadRequest("Course ID mismatch");

            try
            {
                var existingCourse = await _repository.GetCourseByIdAsync(id);
                if (existingCourse == null)
                    return NotFound();

                await _repository.UpdateCourseAsync(course);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Courses/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var existingCourse = await _repository.GetCourseByIdAsync(id);
                if (existingCourse == null)
                    return NotFound();

                await _repository.DeleteCourseAsync(id);
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
