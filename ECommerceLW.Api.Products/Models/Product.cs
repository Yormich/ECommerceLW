﻿namespace ECommerceLW.Api.Products.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}
