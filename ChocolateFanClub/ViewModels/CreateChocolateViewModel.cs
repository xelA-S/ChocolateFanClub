using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Models;
using System.ComponentModel;

namespace ChocolateFanClub.ViewModels
{
    
    public class CreateChocolateViewModel
    { 
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }

        public Rating? Rating { get; set; }

        public IFormFile Image { get; set; }

        public string? ChocolateImageUrl { get; set; }

        public string? Company { get; set; }

        public Tasty? Tasty { get; set; }

        public int? TastyId { get; set; }

        public string? AppUserId { get; set; }

    }
}
