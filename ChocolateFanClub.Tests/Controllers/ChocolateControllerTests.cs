//using AspNetCore;
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
    public class ChocolateControllerTests
    {
        private readonly ChocolateController _chocolateController;
        private readonly IChocolateRepository _chocolateRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChocolateControllerTests()
        {
            // Dependencies
            _chocolateRepository = A.Fake<IChocolateRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            // SUT
            _chocolateController = new ChocolateController(_chocolateRepository, _photoService,_httpContextAccessor);
        }

        [Fact]
        public void ChocolateController_Index_ReturnsSuccess()
        {
            // Arrange
            var chocolates = A.Fake<IEnumerable<Chocolate>>();
            A.CallTo(() => _chocolateRepository.GetAll()).Returns(chocolates);
            // Act
            var result = _chocolateController.Index();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Theory]
        [InlineData(1)]
        public void ChocolateController_Details_ReturnsSuccess(int id)
        {
            // Arrange
            var chocolate = A.Fake<Chocolate>();
            A.CallTo(() => _chocolateRepository.GetByIdAsync(id)).Returns(chocolate);

            // Act
            var result = _chocolateController.Details(id);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Theory]
        [InlineData(1)]
        public void ChocolateController_Edit_HttpGet_ReturnsSuccess(int id)
        {
            // Arrange
            var chocolate = A.Fake<Chocolate>();
            A.CallTo(() => _chocolateRepository.GetByIdAsync(id)).Returns(chocolate);
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

            // Act
            var result = _chocolateController.Edit(id);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Theory]
        [InlineData(1)]
        public void ChocolateController_Delete_HttpGet_ReturnsSuccess(int id)
        {
            // Arrange
            
            int wrongID = 2;
            var chocolate = A.Fake<Chocolate>();
            A.CallTo(() => _chocolateRepository.GetByIdAsync(id)).Returns(chocolate);

            // Act
            var result = _chocolateController.Delete(id);
            var result2 = _chocolateController.Delete(wrongID);


            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result2.Should().BeOfType<Task<IActionResult>>();

        }

    }
}
