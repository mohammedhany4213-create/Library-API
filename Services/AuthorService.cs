using LibraryApi.DTOs;
using LibraryApi.Repositories;

namespace LibraryApi.Services;
public interface IAuthorService
{
    Task<IEnumerable<AuthorDTO>> GetAllAsync();
    Task<AuthorDTO?> GetByIdAsync(int id);
    Task<AuthorDTO> CreateAuthor(AuthorDTO dto);
    Task<AuthorDTO?> UpdateAuthor(int id ,AuthorDTO dto);
}

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository ;
    }
    public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
    {
        var authors = await _repository.GetAllAsync();
        var results = authors.Select(author => new AuthorDTO
        {
            Name = author.Name ,
            Bio = author.Bio 
        });
        return results ;
    }

    public async Task<AuthorDTO?> GetByIdAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        
        if(author == null)
        {
            return null ;
        }

        var result = new AuthorDTO
        {
            Name = author.Name,
            Bio = author.Bio
        };
        return result ;
    }

    public async Task<AuthorDTO> CreateAuthor(AuthorDTO dto)
    {
        var author = await _repository.CreateAuthor(dto);
        return author ;
    }

    public async Task<AuthorDTO?> UpdateAuthor(int id ,AuthorDTO dto)
    {
        var author = await _repository.UpdateAuthor(id , dto);
        return author  ;
    }
}