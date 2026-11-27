using Microsoft.AspNetCore.Mvc;
using LibraryApi.Repositories;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository _repository ;

    public CategoryController(CategoryRepository repository)
    {
        _repository = repository ;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _repository.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if(category == null)
        {
            return NotFound("category not found");
        }
        return Ok(category);
    }
}