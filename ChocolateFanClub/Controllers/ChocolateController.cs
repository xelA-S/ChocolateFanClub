using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChocolateFanClub.Controllers
{
    public class ChocolateController : Controller
    {
        private readonly IChocolateRepository _chocolateRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChocolateController(IChocolateRepository chocolateRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _chocolateRepository = chocolateRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Chocolate> chocolates = await _chocolateRepository.GetAll();
            return View(chocolates);
        }

        public async Task<IActionResult> Details(int id)
        {
            Chocolate chocolate = await _chocolateRepository.GetByIdAsync(id);
            return View(chocolate);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createChocolateViewModel = new CreateChocolateViewModel { AppUserId = currentUserId };
            return View(createChocolateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChocolateViewModel chocoVM)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(chocoVM.Image);
                var chocolate = new Chocolate
                {
                    Name = chocoVM.Name,
                    Description = chocoVM.Description,
                    Rating = chocoVM.Rating,
                    Price = chocoVM.Price,
                    AppUserId = chocoVM.AppUserId,
                    ChocolateImageUrl = result.Url.ToString(),
                    Company = chocoVM.Company,
                    Tasty = new Tasty()
                    {
                        IsTasty = chocoVM.Tasty.IsTasty
                    }

                };

                _chocolateRepository.Add(chocolate);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed.");
            }
            return View(chocoVM);
        }




        public async Task<IActionResult> Edit(int id) 
        {
            Chocolate chocolate = await _chocolateRepository.GetByIdAsync(id);
            if (chocolate == null) return View("Error");
            var chocoVM = new EditChocolateViewModel()
            {
                Id = id,
                Name = chocolate.Name,
                Description = chocolate.Description,
                Rating = chocolate.Rating,
                Price = chocolate.Price,
                ChocolateImageUrl = chocolate.ChocolateImageUrl,
                Company = chocolate.Company
            };
            return View(chocoVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditChocolateViewModel chocoVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Edit failed");
                return View("Error");
            }
            Chocolate currentChocolate = await _chocolateRepository.GetByIdAsyncNoTracking(id);
            if (currentChocolate != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(currentChocolate.ChocolateImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(chocoVM);
                }

            //var photoResult = await _photoService.AddPhotoAsync(chocoVM.Image);
            //var chocolate = new Chocolate()
            //{
            //    Id = id,
            //    Name = chocoVM.Name,
            //    Description = chocoVM.Description,
            //    Rating = chocoVM.Rating,
            //    Price = chocoVM.Price,
            //    ChocolateImageUrl = photoResult.Url.ToString(),
            //    Company = chocoVM.Company
            //};
            await _chocolateRepository.Update(chocoVM);
            return RedirectToAction("Index");
            }
            else
            {
                return View(chocoVM);
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var chocolate = await _chocolateRepository.GetByIdAsync(id);
            if (chocolate == null) return View("Error");
            return View(chocolate);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteChocolate(int id)
        {
            var chocolate = await _chocolateRepository.GetByIdAsync(id);
            if (chocolate == null) return View("Error");
            _chocolateRepository.Delete(chocolate);
            return RedirectToAction("Index");
        }

    }
}
