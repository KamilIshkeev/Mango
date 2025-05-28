using System.ComponentModel.DataAnnotations;

namespace MangoApi.Models
{
    public class Levels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
