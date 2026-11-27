using Dapper;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LibraryApi.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);

    Task<AuthorDTO> CreateAuthor(AuthorDTO dto);

    Task<AuthorDTO?> UpdateAuthor(int id ,AuthorDTO dto);
}

public class AuthorRepository : IAuthorRepository
{
    private readonly string _connectionString;

    public AuthorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT Id, Name, Bio FROM Authors";
        return await connection.QueryAsync<Author>(sql);
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Authors WHERE Id = @id";
        return await connection.QueryFirstOrDefaultAsync<Author>(
            sql ,
            new {Id = id});
    }


    public async Task<AuthorDTO> CreateAuthor(AuthorDTO dto)
    {
    using var connection = new SqlConnection(_connectionString);

    var sql = @"
        INSERT INTO Authors (Name, Bio)
        OUTPUT INSERTED.Name, INSERTED.Bio
        VALUES (@Name, @Bio);
    ";

    return await connection.QuerySingleAsync<AuthorDTO>(sql, dto);
    }

    public async Task<AuthorDTO?> UpdateAuthor(int id ,AuthorDTO dto)
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = @"
        UPDATE Authors 
        SET Name = @Name
            Bio = @Bio
        OUTPUT INSERTED.Name, INSERTED.Bio
        WHERE Id = @id
        ";
        
        return await connection.QuerySingleOrDefaultAsync<AuthorDTO>(
        sql,
        new
        {
            Id = id,
            dto.Name,
            dto.Bio
        });

    }

}