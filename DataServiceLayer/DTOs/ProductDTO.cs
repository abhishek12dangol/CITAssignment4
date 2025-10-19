namespace DataServiceLayer.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
    public int UnitsInStock { get; set; }
    public string CategoryName { get; set; }
    public CategoryDTO Category { get; set; }
}