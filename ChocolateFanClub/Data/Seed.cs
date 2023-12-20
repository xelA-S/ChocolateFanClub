using ChocolateFanClub.Data.Enum;
using ChocolateFanClub.Data;
using ChocolateFanClub.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Net;

namespace RunGroopWebApp.Data
{
    // if there is no data present, this is the default data which will be entered
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();

                context.Database.EnsureCreated();

                if (!context.Chocolates.Any())
                {
                    context.Chocolates.AddRange(new List<Chocolate>()
                    {
                        new Chocolate()
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
                            
                         },
                        new Chocolate()
                        {
                            Name = "Nomo Cookie Dough",
                            ChocolateImageUrl = "https://digitalcontent.api.tesco.com/v2/media/ghs/f70523d3-4ee3-4298-9c67-cfa6207fb1d3/ff02573c-c67f-425b-9dbd-aff6813f0fda_1179039659.jpeg?h=960&w=960",
                            Description = "This is chocolate made by Nomo, if you like the taste of cookies then this one is the the chocolate for you!",
                            Price = 3.00,
                            Company = "Nomo",
                            Rating = Rating.Ten,
                            Tasty = new Tasty()
                            {
                                IsTasty = "very",
                            }
                        },
                        new Chocolate()
                        {
                            Name = "Booja Booja Hazelnut Crunch Truffles",
                            ChocolateImageUrl = "https://www.ocado.com/productImages/713/71344011_0_640x640.jpg?identifier=ead27f0384e4857853ca0b0195bb0b2e",
                            Description = "This is chocolate made by Booja Booja, a bit expensive but it doesnt get better than this!",
                            Price = 3.00,
                            Company = "Booja Booja",
                            Rating = Rating.Ten,
                            Tasty = new Tasty()
                            {
                                IsTasty = "very",
                            }
                        },
                        new Chocolate()
                        {
                            Name = "Kitkat Vegan",
                            ChocolateImageUrl = "https://www.megaretailer.com/media/catalog/product/cache/a6f4aec1db93cb13677a62a0babd5631/U/-/U-2NE3K4FV-15_11_2022_14_13_34_1.jpg",
                            Description = "This is chocolate made by Kitkat, its a standard and cheap chocolate that tastes nice but is average overall.",
                            Price = 1.00,
                            Company = "Nestle",
                            Rating = Rating.Five,
                            Tasty = new Tasty()
                            {
                                IsTasty = "very",
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Shops
                if (!context.Shops.Any())
                {
                    context.Shops.AddRange(new List<Shop>()
                    {
                        new Shop()
                        {
                            Name = "Asda",
                            ShopImageUrl = "https://www.thejunctionshopping.com/wp-content/uploads/2017/03/Asda-1.png",
                            Description ="Good supermarket with many chocolate options",
                            Location = "England",
                            Expensive = new Expensive()
                            {
                                IsExpensive = "Not really"
                            }
                        },
                        new Shop()
                        {
                            Name = "Tesco",
                            ShopImageUrl = "https://images.easyfundraising.org.uk/retailer/cropped/logo-tesco-groceries.svg.png",
                            Description ="Good supermarket with many chocolate options",
                            Location = "England",
                            Expensive = new Expensive()
                            {
                                IsExpensive = "Kinda"
                            }
                        },
                        new Shop()
                        {
                            Name = "Sainsburys",
                            ShopImageUrl = "https://wl3-cdn.landsec.com/sites/default/files/images/shops/logos/sainsburys_1.png",
                            Description ="Good supermarket with many chocolate options",
                            Location = "England",
                            Expensive = new Expensive()
                            {
                                IsExpensive = "Not really"
                            }
                        },
                        new Shop()
                        {
                            Name = "Morrisons",
                            ShopImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/82/MorrisonsLogo.svg",
                            Description ="Good supermarket with many chocolate options",
                            Location = "England",
                            Expensive = new Expensive()
                            {
                                IsExpensive = "Not really"
                            }
                        },
                        new Shop()
                        {
                            Name = "Online",
                            ShopImageUrl = "https://cdn-icons-png.flaticon.com/512/2282/2282299.png",
                            Description ="Has everything",
                            Location = "Internet",
                            Expensive = new Expensive()
                            {
                                IsExpensive = "sometimes"
                            }
                        },
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "alexadmin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        City = "London",
                        ProfileImageUrl = "https://icons.veryicon.com/png/o/internet--web/prejudice/user-128.png"

                    };
                    await userManager.CreateAsync(newAdminUser, "Example123_");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "alexuser@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "chocolover1334",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        City = "London",
                        ProfileImageUrl = "https://icons.veryicon.com/png/o/internet--web/prejudice/user-128.png"


                    };
                    await userManager.CreateAsync(newAppUser, "Example123_");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}