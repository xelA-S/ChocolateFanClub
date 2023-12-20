using ChocolateFanClub.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocolateFanClub.Models
{
    public class Chocolate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        public double? Price { get; set; }

        public Rating? Rating { get; set; }

        public string? ChocolateImageUrl{  get; set; }

        //public List<Shop>? Locations { get; set; }
        [ForeignKey("Tasty")]
        public int? TastyId { get; set; }
        public Tasty? Tasty { get; set; }
        public string? Company {  get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
