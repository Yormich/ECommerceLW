using ECommerceLW.Api.Orders.Models;

namespace ECommerceLW.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order>? Orders, string? ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
