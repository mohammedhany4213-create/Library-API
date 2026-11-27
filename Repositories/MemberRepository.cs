using LibraryApi.Models;
using Dapper ;
using Microsoft.Data.SqlClient;

namespace LibraryApi.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);

}

public class MemberRepository : IMemberRepository
{
    private readonly string _connectionString ;

    public MemberRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Members";
        return await connection.QueryAsync<Member>(sql);
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Members WHERE Id = @id";
        return await connection.QueryFirstOrDefaultAsync<Member>(
            sql ,
            new {Id = id});
    }
}