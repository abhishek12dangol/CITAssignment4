using Microsoft.EntityFrameworkCore;
using DataServiceLayer.Models;
using System;

namespace DataServiceLayer;

public class NorthwindContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql("Host=localhost;Database=northwind;Username=postgres;Password=admin");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Product
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(p => p.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(p => p.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>().Property(p => p.UnitsInStock).HasColumnName("unitsinstock");
        modelBuilder.Entity<Product>().Property(p => p.CategoryId).HasColumnName("categoryid");
        
        // Configure Category
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(c => c.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(c => c.Description).HasColumnName("description");
        
        // Configure Order
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<Order>().Property(o => o.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>().Property(o => o.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Order>().Property(o => o.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Order>().Property(o => o.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>().Property(o => o.ShipCity).HasColumnName("shipcity");
        
        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>().Property(o => o.OrderId).HasColumnName("orderid");
        modelBuilder.Entity<OrderDetails>().Property(o => o.ProductId).HasColumnName("productid");
        modelBuilder.Entity<OrderDetails>().Property(o => o.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetails>().Property(o => o.Quantity).HasColumnName("quantity");
        modelBuilder.Entity<OrderDetails>().Property(o => o.Discount).HasColumnName("discount");

        // Defining Relation between Product and Category
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        // Defining Relationship between Order and OrderDetails
        modelBuilder.Entity<Order>().HasMany(o => o.OrderDetails).WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);
        
        // Composite Key
        modelBuilder.Entity<OrderDetails>().HasKey(od => new {od.OrderId, od.ProductId});
    }
}