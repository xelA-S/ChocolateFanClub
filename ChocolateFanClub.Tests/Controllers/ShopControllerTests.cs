using ChocolateFanClub.Controllers;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.Repository;
using ChocolateFanClub.Services;
using ChocolateFanClub.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFanClub.Tests.Controllers
{
    public class ShopControllerTests
    {
        private readonly ShopController _shopController;
        private readonly IShopRepository _shopRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShopControllerTests()
        {
            // Dependencies
            _shopRepository = A.Fake<IShopRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            // SUT
            _shopController = new ShopController(_shopRepository, _photoService, _httpContextAccessor);
        }

        [Fact]
        public void ShopController_Index_ReturnsSucesss()
        {
            // Arrange
            var shops = A.Fake<IEnumerable<Shop>>();
            A.CallTo(() => _shopRepository.GetAll()).Returns(shops);

            // Act
            var result = _shopController.Index();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
          
        }

        [Theory]
        [InlineData(1)]
        public void ShopController_Details_ReturnsSuccess(int id)
        {
            // Arrange
            var shop = A.Fake<Shop>();
            A.CallTo(() => _shopRepository.GetByIdAsync(id)).Returns(shop);

            // Act
            var result = _shopController.Details(id);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Theory]
        [InlineData(1)]
        public void ShopController_Edit_HttpGet_ReturnsSuccess(int id)
        {
            // Arrange
            var shop = A.Fake<Shop>();
            A.CallTo(() => _shopRepository.GetByIdAsync(id)).Returns(shop);
            var shopVM = new EditShopViewModel()
            {
                Name = shop.Name,
                Description = shop.Description,
                Location = shop.Location,
                ShopImageUrl = shop.ShopImageUrl,
                Expensive = shop.Expensive,

            };
            // Assert
            var result = _shopController.Edit(id);

            // Act
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Theory]
        [InlineData(1)]
        public void ShopController_Delete_HttpGet_ReturnsSuccess(int id)
        {
            // Arrange
            int wrongID = 2;
            var shop = A.Fake<Shop>();
            A.CallTo(() => _shopRepository.GetByIdAsync(id)).Returns(shop);

            // Act
            var result = _shopController.Delete(id);
            var result2 = _shopController.Delete(wrongID);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result2.Should().BeOfType<Task<IActionResult>>();

        }


    }
}
