using AutoMapper;
using ECommerceLW.Api.Products.Db;
using ECommerceLW.Api.Products.Interfaces;
using ECommerceLW.Api.Products.Profiles;
using ECommerceLW.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerceLW.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async void GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile(); 
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext,
                null, mapper);

            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products!.Any());
            Assert.Null(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}