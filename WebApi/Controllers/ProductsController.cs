using DataServiceLayer.DTOs;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IDataService _dataService;

    public ProductsController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("category/{id}")]
    public IActionResult GetProductsByCategory(int id)
    {
        var products = _dataService.GetProductByCategory(id);
        if (products == null || !products.Any())
        {
            return NotFound();
        }
        return Ok(products);
    }

    [HttpGet]
    public IActionResult GetProductsByName([FromQuery] string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Name parameter is required");
        }
        
        var products = _dataService.GetProductByName(name);
        if (products == null || !products.Any())
        {
            return NotFound();
        }
        return Ok(products);
    }
}