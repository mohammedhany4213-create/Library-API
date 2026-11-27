using Microsoft.AspNetCore.Mvc;
using LibraryApi.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly MemberRepository _repository ;

    public MemberController(MemberRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await _repository.GetAllAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( int id)
    {
        var member = await _repository.GetByIdAsync(id);
        if(member == null)
        {
            return NotFound("Member not found");
        }
        return Ok(member);
    }
    
}