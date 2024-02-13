using ChocolateFanClub.Data;
using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using ChocolateFanClub.Repository;
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
    public class ShopRepositoryTests
    {
        private async Task<ApplicationDBContext> GetDBContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDBContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Shops.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Shops.Add(new Shop()
                    {
                        Name = "Asda",
                        ShopImageUrl = "https://www.thejunctionshopping.com/wp-content/uploads/2017/03/Asda-1.png",
                        Description = "Good supermarket with many chocolate options",
                        Location = "England",
                        Expensive = new Expensive()
                        {
                            IsExpensive = "Not really"
                        }
                    });
                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;

        }

        [Fact]
        public async void ShopRepository_Add_ReturnsBool()
        {
            // Arrange
            var shop = new Shop()
            {
                Name = "Asda",
                ShopImageUrl = "https://www.thejunctionshopping.com/wp-content/uploads/2017/03/Asda-1.png",
                Description = "Good supermarket with many chocolate options",
                Location = "England",
                Expensive = new Expensive()
                {
                    IsExpensive = "Not really"
                }
            };
            var dbContext = await GetDBContext();
            var shopRepository = new ShopRepository(dbContext);

            // Act
            var result = shopRepository.Add(shop);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ShopRepository_Delete_ReturnsBool()
        {
            // Arrange
            int id = 1;
            var shop = new Shop()
            {
                Name = "Asda",
                ShopImageUrl = "https://www.thejunctionshopping.com/wp-content/uploads/2017/03/Asda-1.png",
                Description = "Good supermarket with many chocolate options",
                Location = "England",
                Expensive = new Expensive()
                {
                    IsExpensive = "Not really"
                }
            };
            var dbContext = await GetDBContext();
            var shopRepository = new ShopRepository(dbContext);
            //var shop = await shopRepository.GetByIdAsync(id);


            // Act
            shopRepository.Add(shop);
            var result = shopRepository.Delete(shop);

            // Assert
            result.Should().BeTrue();
        }


        [Theory]
        [InlineData(1)]
        public async void ShopRepository_GetByIdAsync_ReturnsShop(int id)
        {
            // Arrange
            var dbContext = await GetDBContext();
            var shopRepository = new ShopRepository(dbContext);
            // Act
            var result = shopRepository.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Shop>>();
        }
    }
}
