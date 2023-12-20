using ChocolateFanClub.Models;

namespace ChocolateFanClub.Interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAll();
        Task<Shop> GetByIdAsync(int id);
        Task<Shop> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Shop>> GetByNameAsync(string name);
        Task<IEnumerable<Shop>> GetByNameAsyncNoTracking(string name);

        bool Add(Shop chocolate);
        bool Update(Shop chocolate);
        bool Delete(Shop chocolate);
        bool Save();
    }
}
