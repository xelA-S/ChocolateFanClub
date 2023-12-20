using ChocolateFanClub.Data;
using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChocolateFanClub.Repository
{
    public class ChocolateRepository : IChocolateRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IPhotoService _photoService;

        public ChocolateRepository(ApplicationDBContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }
        public bool Add(Chocolate chocolate)
        {
            _context.Add(chocolate);
            return Save();
        }

        public bool Delete(Chocolate chocolate)
        {
            _context.Remove(chocolate);
            return Save();
        }

        public async Task<IEnumerable<Chocolate>> GetAll()
        {
            return await _context.Chocolates.ToListAsync();
        }

        public async Task<Chocolate> GetByIdAsync(int id)
        {
            return await _context.Chocolates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chocolate> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Chocolates.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }



        public async Task<IEnumerable<Chocolate>> GetByNameAsync(string name)
        {
            return await _context.Chocolates.Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Chocolate>> GetByNameAsyncNoTracking(string name)
        {
            return await _context.Chocolates.Where(c => c.Name.Contains(name)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Chocolate>> GetChocolateByCompany(string company)
        {
            return await _context.Chocolates.Where(c => c.Company.Contains(company)).ToListAsync();
        }

        public async Task<IEnumerable<Chocolate>> GetChocolateByRating(Rating rating)
        {
            return await _context.Chocolates.Where(c => c.Rating == rating).ToListAsync();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> Update(EditChocolateViewModel chocoVM)
        {
            var photoResult = await _photoService.AddPhotoAsync(chocoVM.Image);
            var something = await _context.Chocolates.Where(a => a.Id == chocoVM.Id).FirstOrDefaultAsync();
            
            var chocolate = new Chocolate()
            {
                Id = chocoVM.Id,
                Name = chocoVM.Name,
                Description = chocoVM.Description,
                Rating = chocoVM.Rating,
                Price = chocoVM.Price,
                ChocolateImageUrl = photoResult.Url.ToString(),
                Company = chocoVM.Company
            };
            _context.Update(chocolate);
            return Save();
        }
    }
}
