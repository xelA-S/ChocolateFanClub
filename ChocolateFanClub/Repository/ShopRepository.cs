using ChocolateFanClub.Data;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFanClub.Repository
{
    public class ShopRepository : IShopRepository 
    {
        private readonly ApplicationDBContext _context;

        public ShopRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool Add(Shop shop)
        {
            _context.Add(shop);
            return Save();
        }

        public bool Delete(Shop shop)
        {
            _context.Remove(shop);
            return Save();
        }

        public async Task<IEnumerable<Shop>> GetAll()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<Shop> GetByIdAsync(int id)
        {
            return await _context.Shops.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Shop> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Shops.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Shop>> GetByNameAsync(string name)
        {
            return await _context.Shops.Where(c => c.Name.Contains(name)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetByNameAsyncNoTracking(string name)
        {
            return await _context.Shops.Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Shop chocolate)
        {
            _context.Update(chocolate);
            return Save();
        }
    }
}
