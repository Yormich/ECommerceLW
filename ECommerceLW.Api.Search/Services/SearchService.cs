﻿using ECommerceLW.Api.Search.Interfaces;

namespace ECommerceLW.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var customersResult = await customersService.GetCustomerAsync(customerId);
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders!)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? productsResult.Products!
                            .FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? "Name Not Found" : "Product information is not available";
                    }
                }

                var result = new
                {
                    Customer = customersResult.IsSuccess ? customersResult.Customer : 
                        new {Name = "Customer information is not available"},
                    ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
