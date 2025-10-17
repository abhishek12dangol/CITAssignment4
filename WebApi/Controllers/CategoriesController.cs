using DataServiceLayer.DTOs;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IDataService _dataService;

    public CategoriesController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _dataService.GetCategories();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryDTO category)
    {
        if (category == null)
        {
            return BadRequest();
        }

        var createdCategory = _dataService.CreateCategory(category.Name, category.Description);
        
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO category)
    {
        if (category == null || id != category.Id)
        {
            return BadRequest();
        }

        var existingCategory = _dataService.GetCategory(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        _dataService.UpdateCategory(id, category.Name, category.Description);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var existingCategory = _dataService.GetCategory(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        _dataService.DeleteCategory(id);
        return Ok();
    }
}