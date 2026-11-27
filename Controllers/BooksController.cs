using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BookController : ControllerBase
{
    private readonly BookRepository _repository ;
    public BookController(BookRepository repository)
    {
        _repository = repository ;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _repository.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if(book == null)
        {
            return NotFound("Book not found");
        }
        return Ok(book);
    }
    

}