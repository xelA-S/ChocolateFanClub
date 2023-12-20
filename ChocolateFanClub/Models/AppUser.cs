using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace ChocolateFanClub.Models
{
    public class AppUser : IdentityUser
    {

        //[Key]
        //public string Id { get; set; }
        public string? City { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Chocolate> Chocolates { get; set; }

        public ICollection<Shop> Shops { get; set; }

    }
}
