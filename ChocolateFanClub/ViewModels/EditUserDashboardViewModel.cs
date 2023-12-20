using ChocolateFanClub.Models;

namespace ChocolateFanClub.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public string? City { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Chocolate> Chocolates { get; set; }

        public ICollection<Shop> Shops { get; set; }

    }
}
