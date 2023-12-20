using ChocolateFanClub.Data;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFanClub.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        // HttpContextAccessor is a giant object which provides information about the webpage
        public DashboardRepository(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Chocolate>> GetAllUserChocolates()
        {

            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userChocolates = await _context.Chocolates.Where(r => r.AppUser.Id == currentUserId).ToListAsync();
            return userChocolates;
        }
      
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            // this saves changes made by other methods, if changes were made then value will be greater than 1 
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            // this is used to make updates to the database with new data
            _context.Users.Update(user);
            return Save();
        }
    }
}
