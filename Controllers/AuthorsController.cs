using LibraryApi.DTOs;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);

        if (author is null)
            return NotFound("Author not found");

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorDTO dto)
    {
        var author = await _service.CreateAuthor(dto);
        return Ok(author);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id , AuthorDTO dto)
    {
        var author = await _service.UpdateAuthor(id , dto);
        if (author is null)
            return NotFound("Author not found");

        return Ok(author);
    }
}
