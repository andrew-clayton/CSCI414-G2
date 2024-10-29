using BookExchangeApi.Models;
using BookExchangeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationsRepository _repository;

        public NotificationsController(NotificationsRepository repository)
        {
            _repository = repository;
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            try
            {
                await _repository.CreateNotificationAsync(notification);
                return CreatedAtAction(nameof(GetNotificationById), new { id = notification.NotificationId }, notification);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Notifications/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            try
            {
                var notification = await _repository.GetNotificationByIdAsync(id);
                if (notification == null)
                    return NotFound();

                return Ok(notification);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Notifications/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] Notification notification)
        {
            if (id != notification.NotificationId)
                return BadRequest("Notification ID mismatch");

            try
            {
                var existingNotification = await _repository.GetNotificationByIdAsync(id);
                if (existingNotification == null)
                    return NotFound();

                await _repository.UpdateNotificationAsync(notification);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Notifications/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                var existingNotification = await _repository.GetNotificationByIdAsync(id);
                if (existingNotification == null)
                    return NotFound();

                await _repository.DeleteNotificationAsync(id);
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
