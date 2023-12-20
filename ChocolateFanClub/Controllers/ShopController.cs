using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.Repository;
using ChocolateFanClub.Services;
using ChocolateFanClub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFanClub.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopRepository _shopRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopController(IShopRepository shopRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _shopRepository = shopRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Shop> shops = await _shopRepository.GetAll();
            return View(shops);
        }

        public async Task<IActionResult> Details(int id)
        {
            Shop shop = await _shopRepository.GetByIdAsync(id);
            return View(shop);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var createShopViewModel = new CreateShopViewModel() { };
            return View(createShopViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShopViewModel shopVM)
        {

            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(shopVM.Image);
                var shop = new Shop
                {
                    Name = shopVM.Name,
                    Description = shopVM.Description,
                    Location = shopVM.Location,
                    ShopImageUrl = result.Url.ToString(),
                    Expensive = new Expensive()
                    {
                        IsExpensive = shopVM.Expensive.IsExpensive
                    }

                };

                _shopRepository.Add(shop);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed.");
            }
            return View(shopVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Shop shop = await _shopRepository.GetByIdAsync(id);
            if (shop == null) return View("Error");
            var shopVM = new EditShopViewModel()
            {
                Name = shop.Name,
                Description = shop.Description,
                Location = shop.Location,
                ShopImageUrl = shop.ShopImageUrl,
                Expensive = shop.Expensive,

            };
            return View(shopVM);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditShopViewModel shopVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Edit failed");
                return View("Error");
            }
            Shop currentShop = await _shopRepository.GetByIdAsyncNoTracking(id);
            if (currentShop != null)
            {
                //try
                //{
                //    await _photoService.DeletePhotoAsync(currentShop.ShopImageUrl);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", "Could not delete photo");
                //    return View(chocoVM);
                //}

                var photoResult = await _photoService.AddPhotoAsync(shopVM.Image);
                var shop = new Shop()
                {
                    Id = id,
                    Name = shopVM.Name,
                    Description = shopVM.Description,
                    Location = shopVM.Location,
                    ShopImageUrl = photoResult.Url.ToString(),
                    Expensive = new Expensive()
                    {
                        IsExpensive = shopVM.Expensive.IsExpensive,
                    }
                };
                _shopRepository.Update(shop);
                return RedirectToAction("Index");
            }
            else
            {
                return View(shopVM);
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var shop = await _shopRepository.GetByIdAsync(id);
            if (shop == null) return View("Error");
            return View(shop);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteChocolate(int id)
        {
            var shop = await _shopRepository.GetByIdAsync(id);
            if (shop == null) return View("Error");
            _shopRepository.Delete(shop);
            return RedirectToAction("Index");
        }


    }
}

