using ChocolateFanClub.Data;
using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.Repository;
using ChocolateFanClub.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFanClub.Tests.Repositories
{
    public class ChocolateRepositoryTests
    {
        private async Task<ApplicationDBContext> GetDBContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDBContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Chocolates.CountAsync() < 1)
            {
                for(int i = 0; i < 10; i++)
                {
                databaseContext.Chocolates.Add(new Chocolate()
                {
                    Name = "Loveraw Wafer Bars",
                    ChocolateImageUrl = "https://eatloveraw.com/cdn/shop/files/1200x675px_CFW_Collection_1_600x.png?v=1653040533",
                    Description = "This is chocolate made by Loveraw, very similar to kinder bueno and just as tasty!",
                    Price = 1.99,
                    Company = "Loveraw",
                    Rating = Rating.Eight,
                    Tasty = new Tasty()
                    {
                        IsTasty = "very",
                    }

                });
                await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;

        }
        private readonly IPhotoService _photoService;

        public ChocolateRepositoryTests()
        {
            // Dependencies
            _photoService = A.Fake<IPhotoService>();
            
        }

        [Fact]
        public async void ChocolateRepository_Add_ReturnsBool()
        {
            // Arrange
            var chocolate = new Chocolate()
            {
                Name = "Loveraw Wafer Bars",
                ChocolateImageUrl = "https://eatloveraw.com/cdn/shop/files/1200x675px_CFW_Collection_1_600x.png?v=1653040533",
                Description = "This is chocolate made by Loveraw, very similar to kinder bueno and just as tasty!",
                Price = 1.99,
                Company = "Loveraw",
                Rating = Rating.Eight,
                Tasty = new Tasty()
                {
                    IsTasty = "very",
                }
            };
            var dbContext = await GetDBContext();
            var chocolateRepository = new ChocolateRepository(dbContext, _photoService);

            // Act
            var result = chocolateRepository.Add(chocolate);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ChocolateRepository_Delete_ReturnsBool()
        {
            // Arrange
            var chocolate = new Chocolate()
            {
                Name = "Loveraw Wafer Bars",
                ChocolateImageUrl = "https://eatloveraw.com/cdn/shop/files/1200x675px_CFW_Collection_1_600x.png?v=1653040533",
                Description = "This is chocolate made by Loveraw, very similar to kinder bueno and just as tasty!",
                Price = 1.99,
                Company = "Loveraw",
                Rating = Rating.Eight,
                Tasty = new Tasty()
                {
                    IsTasty = "very",
                }
            };
            var dbContext = await GetDBContext();
            var chocolateRepository = new ChocolateRepository(dbContext, _photoService);

            // Act
            chocolateRepository.Add(chocolate);
            var result = chocolateRepository.Delete(chocolate);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        public async void ChocolateRepository_GetByIdAsync_ReturnsChocolate(int id)
        {
            // Arrange
            
            var dbContext = await GetDBContext();
            var chocolateRepository = new ChocolateRepository(dbContext, _photoService);
            // Act
            var result = chocolateRepository.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Chocolate>>();
        }
    }
}
