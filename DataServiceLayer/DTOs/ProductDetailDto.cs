using DataServiceLayer.Models;

namespace DataServiceLayer.DTOs;

public class ProductDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
    public int UnitsInStock { get; set; }
    public string CategoryName { get; set; }
}

public class ProductByNameDTO
{
    public string ProductName { get; set; }
}

public class ProductByCategoryDTO
{
    public string Name { get; set; }
    public string CategoryName { get; set; }
}

