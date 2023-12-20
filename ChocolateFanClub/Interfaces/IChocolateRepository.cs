using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;

namespace ChocolateFanClub.Interfaces
{
    public interface IChocolateRepository
    {
        Task<IEnumerable<Chocolate>> GetAll();
        Task<Chocolate> GetByIdAsync(int id);
        Task<Chocolate> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Chocolate>> GetByNameAsync(string name);
        Task<IEnumerable<Chocolate>> GetByNameAsyncNoTracking(string name);
        Task<IEnumerable<Chocolate>> GetChocolateByCompany(string company);
        Task<IEnumerable<Chocolate>> GetChocolateByRating(Rating rating);



        bool Add(Chocolate chocolate);
        Task<bool> Update(EditChocolateViewModel chocoVM);
        bool Delete(Chocolate chocolate);
        bool Save();


    }
}
