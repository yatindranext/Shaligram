using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductManagementAPI.Model
{
    public class DapperDbContext
    {
        private readonly IConfiguration _config;
        public DapperDbContext(IConfiguration config) => _config = config;

        public IDbConnection CreateConnection() => new SqlConnection(_config.GetConnectionString("ConnectionStrings"));
    }

}
