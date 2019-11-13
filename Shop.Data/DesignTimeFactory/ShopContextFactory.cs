using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.DesignTimeFactory
{
    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
    {
 
        public ShopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
            optionsBuilder.UseSqlServer("Server =.; Database = ShopDb;Trusted_Connection = True; MultipleActiveResultSets = true");

            return new ShopDbContext(optionsBuilder.Options);
        }
    }
}
