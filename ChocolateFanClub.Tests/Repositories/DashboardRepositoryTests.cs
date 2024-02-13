using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Data;
using ChocolateFanClub.Interfaces;
using ChocolateFanClub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ChocolateFanClub.Repository;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace ChocolateFanClub.Tests.Repositories
{
    public class DashboardRepositoryTests
    {
        private async Task<ApplicationDBContext> GetDBContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDBContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Chocolates.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepositoryTests()
        {
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

        }

        [Fact]
        public async void DashboardRepository_GetAllUserChocolates_ReturnsList()
        {
            // Arrange
            var currentUserId = Guid.NewGuid().ToString();
            var dbContext = await GetDBContext();
            var dashboardRepository = new DashboardRepository(dbContext,_httpContextAccessor);

            // Act
            var result = dashboardRepository.GetAllUserChocolates();

            // Assert
            result.Should().BeOfType<Task<List<Chocolate>>>();
        }
    }
}
