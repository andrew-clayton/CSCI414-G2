using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksRepository _repository;

        public BooksController(BooksRepository repository)
        {
            _repository = repository;
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            try
            {
                await _repository.CreateBookAsync(book);
                return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Books/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _repository.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Books?title=someTitle
        [HttpGet]
        public async Task<IActionResult> GetBooksByTitle([FromQuery] string title)
        {
            try
            {
                var books = await _repository.GetBooksByTitleAsync(title);
                return Ok(books);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Books/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.BookId)
                return BadRequest("Book ID mismatch");

            try
            {
                var existingBook = await _repository.GetBookByIdAsync(id);
                if (existingBook == null)
                    return NotFound();

                await _repository.UpdateBookAsync(book);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Books/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var existingBook = await _repository.GetBookByIdAsync(id);
                if (existingBook == null)
                    return NotFound();

                await _repository.DeleteBookAsync(id);
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
