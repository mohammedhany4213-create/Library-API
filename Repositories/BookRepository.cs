using Dapper;
using LibraryApi.Models;
using Microsoft.Data.SqlClient;

namespace LibraryApi.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync() ;
    Task<Book?> GetByIdAsync(int id);
}

public class BookRepository : IBookRepository
{
    private readonly string _connectionString ;

    public BookRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;

    }
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Books ";
        return await connection.QueryAsync<Book>(sql);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Books WHERE Id = @id";
        return await connection.QueryFirstOrDefaultAsync<Book>(
            sql ,
            new {Id = id});
    }
}