using System.ComponentModel.DataAnnotations;

namespace ChocolateFanClub.Models
{
    public class Tasty
    {
        [Key]
        public int Id { get; set; }
        public string? IsTasty { get; set; }
    }
}
