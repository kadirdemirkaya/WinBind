using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Entities;

namespace WinBind.Persistence.Data
{
    public static class WinBindSeedContext
    {
        public static async Task SeedAsync(WinBindDbContext context)
        {
            if (context.Categories.Count() == 0)
            {
                await context.Categories.AddRangeAsync
                    (
                        new List<Category>()
                        {
                            new()
                            {
                                Id = Guid.Parse("B8ADEF21-797C-4920-8022-B04787F3F49F"),
                                Name = "Quartz Saatler",
                                Description = "Quartz Saatler Açıklama"
                            },
                            new Category()
                            {
                                 Id = Guid.Parse("3DEABC52-A8E6-47DF-A211-FAC748C6C979"),
                                Name = "Otomatik Saatler",
                                Description = "Otomatik Saatler Açıklama"
                            },
                            new()
                            {
                                 Id = Guid.Parse("84FB7643-4D3C-4546-A217-9C151EBD8D59"),
                                Name = "Mekanik Saatler",
                                Description = "Mekanik Saatler Açıklama"
                            },
                            new()
                            {
                                 Id = Guid.Parse("C6976AA0-6CAD-40A4-B728-26A7471A7EA5"),
                                Name = "Akıllı Saatler",
                                Description = "Akıllı Saatler Açıklama"
                            },
                            new()
                            {
                                 Id = Guid.Parse("E3780A9E-653B-432D-890A-F063E5B7A942"),
                                Name = "Pilot Saatler",
                                Description = "Pilot Saatler Açıklama"
                            },
                            new()
                            {
                                 Id = Guid.Parse("82F24E16-D019-4BD5-957D-81931E643AA9"),
                                Name = "Askeri Saatler",
                                Description = "Askeri Saatler Açıklama"
                            }
                        }
                    );

                await context.SaveChangesAsync();
            }
            if (context.Products.Count() == 0)
            {
                await context.Products.AddAsync(new()
                {
                    Id = Guid.Parse("95AF7707-98C8-4833-AC99-12DF0B480296"),
                    CategoryId = Guid.Parse("B8ADEF21-797C-4920-8022-B04787F3F49F"),
                    UserId = Guid.Parse("1278f846-c1e3-465e-df75-08dd2d6b9394"),
                    IsAvailable = true,
                    Name = "Quartz Saat 1",
                    Price = 1000,
                    SKU = "QZ1",
                    ProductImages = new List<ProductImage>()
                    {
                        new()
                        {
                            Path = "random/path/1.png",
                        },
                        new()
                        {
                            Path = "random/path/2.png",
                        }
                    },
                    Description = "Quartz Saat 1 Açıklama"
                });
                await context.Products.AddAsync(new()
                {
                    Id = Guid.Parse("1EA48088-2945-44F9-9BCB-B117B061DAF4"),
                    CategoryId = Guid.Parse("82F24E16-D019-4BD5-957D-81931E643AA9"),
                    UserId = Guid.Parse("1278f846-c1e3-465e-df75-08dd2d6b9394"),
                    IsAvailable = true,
                    Name = "Askeri Saat 2",
                    Price = 2000,
                    SKU = "QZ2",
                    ProductImages = new List<ProductImage>()
                    {
                        new()
                        {
                            Path = "random/path/3.png",
                        },
                        new()
                        {
                            Path = "random/path/4.png",
                        }
                    },
                    Description = "Askeri Saat 2 Açıklama"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
