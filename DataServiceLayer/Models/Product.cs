using System.ComponentModel.DataAnnotations;

namespace DataServiceLayer.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string QuantityPerUnit { get; set; }
    public double UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class ProductByNameDTO
{
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public class ProductByCategoryDTO
{
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
}