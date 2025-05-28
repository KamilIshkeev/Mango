using MangoApi.DataBaseContext;
using MangoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangoApi.Controllers
{
    // Controllers/ChatController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly MangoDbContext _context;

        public ChatController(MangoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMessage()
        {
            var movies = _context.Message.ToList();
            return Ok(movies);
        }

        // Получение всех сообщений для фильма
        [HttpGet("movie/{user_Id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessageByMovie(int user_Id)
        {
            var Message = await _context.Message.Where(m => m.UserId == user_Id)
                .ToListAsync();
            return Ok(Message);
        }

        // Отправка нового сообщения
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage([FromBody] Message message)
        {
            if (message == null) return BadRequest();

            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMessageByMovie), new { UserId = message.UserId }, message);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var mes = await _context.Message.FindAsync(id);
            if (mes == null)
            {
                return BadRequest();
            }
            _context.Message.Remove(mes);
            await _context.SaveChangesAsync();
            return Ok(mes);
        }

    }
}
