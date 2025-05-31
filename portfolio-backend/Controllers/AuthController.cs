using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using portfolio_backend.Models;
using portfolio_backend.settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace portfolio_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly IConfiguration _config = config;

        [HttpPost("token")]
        public ActionResult<string> GenerateToken([FromBody] LoginDTO login)
        {
            Console.WriteLine(login.Email);
            var adminEmail = _config["AdminCredentials:Email"];
            var adminPassword = _config["AdminCredentials:Password"];

            if (login.Email != adminEmail || login.Password != adminPassword)
                return Unauthorized();

            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, login.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }
    }
}
