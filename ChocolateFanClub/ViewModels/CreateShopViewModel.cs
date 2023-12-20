using ChocolateFanClub.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocolateFanClub.ViewModels
{
    public class CreateShopViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ShopImageUrl { get; set; }
        public int? ExpensiveId { get; set; }
        public Expensive? Expensive { get; set; }
        public string? Location { get; set; }
        public IFormFile Image { get; set; }

    }
}
