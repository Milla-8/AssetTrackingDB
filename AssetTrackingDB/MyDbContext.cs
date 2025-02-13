using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
namespace AssetTrackingDB

{
    internal class MyDbContext : DbContext
    {
        string connectionString = "Server=localhost,1433; Database=AssetTrackingDB; User Id=sa; Password=CamillA2025; TrustServerCertificate=true";

        public DbSet<Currency> Currencies { get; set; } // creating Currency table
        public DbSet<Office> Offices { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        { // Seed data into tables

            ModelBuilder.Entity<Currency>().HasData(new Currency { CurrencyId = 1, CurrencyName = "Svenska Kronor", ShortName = "SEK" });
            ModelBuilder.Entity<Currency>().HasData(new Currency { CurrencyId = 2, CurrencyName = "US Dollars", ShortName = "USD" });

            ModelBuilder.Entity<Office>().HasData(new Office { OfficeId = 1, Country = "Sweden", City = "Stockholm", CurrencyId = 1 });
            ModelBuilder.Entity<Office>().HasData(new Office { OfficeId = 2, Country = "Sweden", City = "Malmö", CurrencyId = 1 });
            ModelBuilder.Entity<Office>().HasData(new Office { OfficeId = 3, Country = "USA", City = "New York", CurrencyId = 2 });

            ModelBuilder.Entity<Asset>().HasData(new Asset { AssetId = 1, Category = "Laptop", Brand = "Apple", Model = "MacBook Pro", Configuration = "8 GB RAM, 256GB SSD", Price = 15000, PurchaseDate = "2022-12-10", OfficeId = 1 });
            ModelBuilder.Entity<Asset>().HasData(new Asset { AssetId = 2, Category = "Phone", Brand = "Samsung", Model = "S24", Configuration = "512GB", Price = 20000, PurchaseDate = "2024-11-20", OfficeId = 1 });
            ModelBuilder.Entity<Asset>().HasData(new Asset { AssetId = 3, Category = "Laptop", Brand = "Lenovo", Model = "ThinkPad", Configuration = "16GB RAM, 512GB SSD", Price = 15499, PurchaseDate = "2023-05-17", OfficeId = 2 });
            ModelBuilder.Entity<Asset>().HasData(new Asset { AssetId = 4, Category = "Stationary Computer", Brand = "Apple", Model = "iMac", Configuration = "16GB RAM, 1TB SSD", Price = 25000, PurchaseDate = "2024-02-05", OfficeId = 3 });

        }
    }
}

