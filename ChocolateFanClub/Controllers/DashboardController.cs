using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFanClub.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        // this is used to avoid the issue of two instances of AppUser being manipulated at the same time
        // updates the user info with info inputted into the edit form
        public void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
        }
        public async Task<IActionResult> Index()
        {
            var userChocolates = await _dashboardRepository.GetAllUserChocolates();
            var dashboardViewModel = new DashboardViewModel()
            {
                Chocolates = userChocolates,
            };
            return View(dashboardViewModel);
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currentUserId);
            if (user == null) { return View("Error"); }
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currentUserId,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit user profile");
                return View(editVM);
            }
            // this is an instance of AppUser, no tracking is required to prevent issues of two instances of the same class being manipulated at once
            var currentUser = await _dashboardRepository.GetUserByIdNoTracking(editVM.Id);
            if (currentUser != null)
            {
                if (currentUser.ProfileImageUrl == "" || currentUser.ProfileImageUrl == null)
                {
                    //var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                    //// this is a DTO made to imitate AppUser to prevent two instances of AppUser being manipulated
                    //var user = new EditUserDashboardViewModel()
                    //{
                    //    UserName = editVM.UserName,
                    //    Pace = editVM.Pace,
                    //    Mileage = editVM.Mileage,
                    //    State = editVM.State,
                    //    City = editVM.City

                    //};

                    // this is used to avoid the issue of two instances of AppUser being manipulated at the same time
                    // updates the user info with info inputted into the edit form
                    //MapUserEdit(currentUser, editVM, photoResult);

                    _dashboardRepository.Update(currentUser);
                    return RedirectToAction("Index", "Dashboard");
                }
                // this is if the user has a photo already
                else
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(currentUser.ProfileImageUrl);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(editVM);
                    }
                    // this is if the existing photo was deleted
                    //var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                    //MapUserEdit(currentUser, editVM, photoResult);

                    _dashboardRepository.Update(currentUser);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return View(editVM);
            }


        }
    }
}
