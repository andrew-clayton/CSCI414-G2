using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedBooksController : ControllerBase
    {
        private readonly SavedBooksRepository _repository;

        public SavedBooksController(SavedBooksRepository repository)
        {
            _repository = repository;
        }

        // POST: api/SavedBooks
        [HttpPost]
        public async Task<IActionResult> CreateSavedBook([FromBody] SavedBook savedBook)
        {
            try
            {
                await _repository.CreateSavedBookAsync(savedBook);
                return CreatedAtAction(nameof(GetSavedBooksByStudentId), new { studentId = savedBook.StudentId }, savedBook);
                //return CreatedAtAction(nameof(GetSavedBooksByStudentId), new { studentId = savedBook.StudentId }, savedBook);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/SavedBooks/{studentId}
        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> GetSavedBooksByStudentId(int studentId)
        {
            try
            {
                var savedBooks = await _repository.GetSavedBooksByStudentIdAsync(studentId);
                return Ok(savedBooks);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/SavedBooks/{studentId}/{bookId}
        [HttpDelete("{studentId:int}/{bookId:int}")]
        public async Task<IActionResult> DeleteSavedBook(int studentId, int bookId)
        {
            try
            {
                await _repository.DeleteSavedBookAsync(bookId, studentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                if (ex is KeyNotFoundException)
                    return NotFound();
                else
                    return StatusCode(500, "Internal server error");
            }
        }
    }
}
