using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocolateFanClub.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ShopImageUrl { get; set; }
        [ForeignKey("Expensive")]
        public int? ExpensiveId { get; set; }
        public Expensive? Expensive { get; set; }
        public string? Location { get; set; }
    }
}
