using ChocolateFanClub.Models;

namespace ChocolateFanClub.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Chocolate>> GetAllUserChocolates();

        Task<AppUser> GetUserById(string id);

        Task<AppUser> GetUserByIdNoTracking(string id);

        bool Update(AppUser user);

        bool Save();
    }
}
