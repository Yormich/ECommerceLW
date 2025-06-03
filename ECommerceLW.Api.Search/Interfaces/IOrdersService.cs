using ECommerceLW.Api.Search.Models;

namespace ECommerceLW.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order>? Orders, string? ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
