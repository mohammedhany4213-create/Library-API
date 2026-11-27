using LibraryApi.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LibraryApi.Repositories;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllAsync();
    Task<Loan?> GetByIdAsync(int id);

}

public class LoanRepository : ILoanRepository
{
    private readonly string _connectionString ;

    public LoanRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<IEnumerable<Loan>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Loans";
        return await connection.QueryAsync<Loan>(sql);
    }

    public async Task<Loan?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Loans WHERE Id = @id";
        return await connection.QueryFirstOrDefaultAsync<Loan>(
            sql ,
            new {Id = id});
    }
}