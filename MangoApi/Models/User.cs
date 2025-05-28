using System.ComponentModel.DataAnnotations;

namespace MangoApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Money { get; set; }

    }
}
