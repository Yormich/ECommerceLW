using ECommerceLW.Api.Search.Models;

namespace ECommerceLW.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMessage)> GetProductsAsync();
    }
}
