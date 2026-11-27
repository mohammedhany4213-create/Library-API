using LibraryApi.Models ;
using Dapper ;
using Microsoft.Data.SqlClient;

namespace LibraryApi.Repositories ;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly string _connectionString ;

    public CategoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Categories";
        return await connection.QueryAsync<Category>(sql);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Categories WHERE Id = @id";
        return await connection.QueryFirstOrDefaultAsync<Category>(
            sql ,
            new {Id = id});
    }
}