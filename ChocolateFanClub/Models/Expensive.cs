using System.ComponentModel.DataAnnotations;

namespace ChocolateFanClub.Models
{
    public class Expensive
    {
        [Key]
        public int Id { get; set; }
        public string? IsExpensive {  get; set; }
    }
}
