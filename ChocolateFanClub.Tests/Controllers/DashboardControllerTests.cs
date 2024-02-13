using ChocolateFanClub.Controllers;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFanClub.Tests.Controllers
{
    public class DashboardControllerTests
    {
        private readonly DashboardController _dashboardController;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        public DashboardControllerTests()
        {
            // Dependencies    
            _dashboardRepository = A.Fake<IDashboardRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            // SUT
            _dashboardController = new DashboardController(_dashboardRepository,_httpContextAccessor ,_photoService);

        }

        [Fact]
        public void DashboardController_Index_ReturnsSuccess()
        {
            // Arrange
            var userChocolates = A.Fake<List<Chocolate>>();
            var dashboardViewModel = new DashboardViewModel()
            {
                Chocolates = userChocolates,
            };

            // Act
            var result = _dashboardController.Index();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Fact]
        public void DashboardController_EditUserProfile_Get_ReturnsSuccess()
        {
            // Arrange
            var currentUserId = Guid.NewGuid().ToString();
            var user = A.Fake<AppUser>();
            A.CallTo(() => _dashboardRepository.GetUserById(currentUserId)).Returns(user);
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currentUserId,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
            };
            // Act
            var result = _dashboardController.EditUserProfile();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }

    }
}
