using System.ComponentModel.DataAnnotations.Schema;

namespace MangoApi.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } // Текст сообщения
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; } // ID пользователя
        //// Навигационные свойства
        //[ForeignKey("User_Id")]
        //public User User { get; set; }
    }
}
