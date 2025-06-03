using AutoMapper;
using ECommerceLW.Api.Products.Db;
using ECommerceLW.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceLW.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext db, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            dbContext = db;
            this.mapper = mapper;
            this.logger = logger;

            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 1,
                    Name = "Keyboard",
                    Price = 20,
                    Inventory = 100
                });
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 5,
                    Inventory = 200
                });
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 150,
                    Inventory = 1000
                });
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 4,
                    Name = "CPU",
                    Price = 200,
                    Inventory = 2000
                });
                dbContext.SaveChanges();
            }
        }


        public async Task<(bool IsSuccess, IEnumerable<Models.Product>? Products, string? ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Count > 0)
                {
                    var result = mapper.Map<IEnumerable<Db.Product>,
                   IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product? Product, string? ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
