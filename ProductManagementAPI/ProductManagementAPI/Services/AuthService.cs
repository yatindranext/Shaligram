using Dapper;
using Microsoft.IdentityModel.Tokens;
using ProductManagementAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagementAPI.Services
{
    public class AuthService
    {
        private readonly DapperDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(DapperDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM AdminUsers WHERE Username = @Username AND PasswordHash = @Password",
                new { Username = username, Password = password }
            );
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(ClaimTypes.Name, user.Username) },
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
