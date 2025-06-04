using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangoApi.Models
{
    public class Passinglvl
    {
        [Key]
        public int Id { get; set; }

        public int User_Id { get; set; }
        //// Навигационные свойства
        //[ForeignKey("User_Id")]
        //public User User { get; set; }
        public int Levels_Id { get; set; }
        //// Навигационные свойства
        //[ForeignKey("Levels_Id")]
        //public Levels Levels { get; set; }

        public int CountStars { get; set; }
    }
}
