using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorRepository _repository ;
    public AuthorController(AuthorRepository repository)
    {
        _repository = repository ;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _repository.GetAllAsync();
        return Ok(authors);
    }
}