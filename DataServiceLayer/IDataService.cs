using DataServiceLayer.Models;
using DataServiceLayer;
using DataServiceLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataServiceLayer;

public interface IDataService
{
   ProductDTO GetProduct(int id);
   List<CategoryDTO> GetCategories();
   CategoryDTO GetCategory(int id);
   CategoryDTO CreateCategory(string name, string description);
   bool DeleteCategory(int id);
   bool UpdateCategory(int id, string newName, string newDescription);
  List<DTOs.ProductByCategoryDTO> GetProductByCategory(int id);
   List<DTOs.ProductByNameDTO> GetProductByName(string name);
   Order GetOrder(int id);
   List<Order> GetOrders();
   List<OrderDetails> GetOrderDetailsByOrderId(int id);
   List<OrderDetails> GetOrderDetailsByProductId(int productId);
}

public class DataService : IDataService
{
   private NorthwindContext _db;

   public DataService()
   {
      _db = new NorthwindContext();
   }

   public ProductDTO GetProduct(int id)
   {
      return _db.Products
         .Where(p => p.Id == id)
         .Select(p => new ProductDTO
         {
            Id = p.Id,
            Name = p.Name,
            UnitPrice = (decimal)p.UnitPrice,
            QuantityPerUnit = p.QuantityPerUnit,
            UnitsInStock = p.UnitsInStock,
            CategoryName = p.Category.Name,
            Category = new CategoryDTO
            {
               Name = p.Category.Name
            }
         })
         .FirstOrDefault();
   }

   public List<CategoryDTO> GetCategories()
   {
      return _db.Categories.Where(c => c.Id >= 1 && c.Id <= 8).OrderBy(c => c.Name).Select(c => new CategoryDTO
      {
         Id = c.Id,
         Name = c.Name,
         Description = c.Description
      }).ToList();
   }

   public CategoryDTO GetCategory(int id)
   {
      return _db.Categories.Where(c => c.Id == id).Select(c => new CategoryDTO
      {
         Id = c.Id,
         Name = c.Name,
         Description = c.Description
      }).SingleOrDefault();
   }

   public CategoryDTO CreateCategory(string name, string description)
   {
      var nextId = (_db.Categories.OrderByDescending(c => c.Id).Select(c => c.Id).Max()) + 1;
      
      var category = new Category { Id = nextId, Name = name, Description = description };
      _db.Categories.Add(category);
      _db.SaveChanges();
      
      return new CategoryDTO
      {
         Id = category.Id,
         Name = category.Name,
         Description = category.Description
      };
   }

   public bool DeleteCategory(int id)
   {
      var cat = _db.Categories.Find(id);
      if (cat is null) return false;
      _db.Categories.Remove(cat);
      _db.SaveChanges();
      return true;
   }

   public bool UpdateCategory(int id, string newName, string newDescription)
   {
      var category = _db.Categories.FirstOrDefault(c => c.Id == id);
      if (category is null) return false;
      category.Name = newName;
      category.Description = newDescription;
      _db.SaveChanges();
      return true;
   }

   public List<DTOs.ProductByNameDTO> GetProductByName(string name)
   {
      return _db.Products.Where(p => p.Name.Contains(name)).Select(p => new DTOs.ProductByNameDTO
      {
         ProductName = p.Name
      }).ToList();
   }

   public List<DTOs.ProductByCategoryDTO> GetProductByCategory(int id)
   {
      return _db.Products.Where(p => p.CategoryId == id).Select(p => new DTOs.ProductByCategoryDTO
      {
         Name = p.Name,
         CategoryName = p.Category.Name
      }).ToList();
   }

   public Order GetOrder(int id)
   {
      return _db.Orders
         .Include(o => o.OrderDetails)
         .ThenInclude(od => od.Product)
         .ThenInclude(p => p.Category)
         .SingleOrDefault(o => o.Id == id);
   }

   public List<Order> GetOrders()
   {
      return _db.Orders
         .Select(o => new Order
         {
            Id = o.Id,
            Date = o.Date,
            ShipName = o.ShipName,
            ShipCity = o.ShipCity
         })
         .ToList();
   }

   public List<OrderDetails> GetOrderDetailsByOrderId(int id)
   {
      return _db.OrderDetails
         .Where(od => od.OrderId == id)
         .Include(od => od.Product)
         .ToList();
   }

   public List<OrderDetails> GetOrderDetailsByProductId(int id)
   {
      var query = _db.OrderDetails.Include(od => od.Order)
         .Where(od => od.ProductId == id).ToList();
      return query;
   }
}