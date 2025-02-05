using Dapper;
using ProductManagementAPI.Model;

namespace ProductManagementAPI.Repositories
{
    public class CategoryRepository
    {
        private readonly DapperDbContext _context;
        public CategoryRepository(DapperDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetCategories()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Category>("SELECT * FROM Categories ORDER BY Id DESC");
        }

        public async Task<int> AddCategory(string name)
        {
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync("INSERT INTO Categories (Name) VALUES (@Name)", new { Name = name });
        }
        public async Task<int> UpdateCategory(int id, string name)
        {
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync("UPDATE Categories SET Name = @Name WHERE Id = @Id", new { Id = id, Name = name });
        }

        public async Task<int> DeleteCategory(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync("DELETE FROM Categories WHERE Id = @Id", new { Id = id });
        }
    }

}
