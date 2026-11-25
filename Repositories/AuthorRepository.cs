using Dapper;
using LibraryApi.Models;
using Microsoft.Data.SqlClient;

namespace LibraryApi.Repositories;

public class AuthorRepository
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
}