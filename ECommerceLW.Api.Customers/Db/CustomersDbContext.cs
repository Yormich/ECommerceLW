﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceLW.Api.Customers.Db
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomersDbContext(DbContextOptions options) : base(options)
        {
        }
    }

}
