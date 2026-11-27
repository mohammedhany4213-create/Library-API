using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LoanController : ControllerBase
{
    private readonly LoanRepository _repository;

    public LoanController(LoanRepository repository)
    {
        _repository = repository ;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var loans = await _repository.GetAllAsync();
        return Ok(loans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( int id)
    {
        var loan = await _repository.GetByIdAsync(id);
        if(loan == null)
        {
            return NotFound("loan not found");
        }
        return Ok(loan);
    }
    
}