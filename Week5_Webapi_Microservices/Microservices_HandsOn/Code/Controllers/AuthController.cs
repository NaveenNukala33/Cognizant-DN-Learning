using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthAPI.Models;

namespace JwtAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (IsValidUser(model))
            {
                var token = GenerateJwtToken(model.Username);
                var user = GetUserInfo(model.Username);
                
                return Ok(new 
                { 
                    Token = token,
                    User = user,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]))
                });
            }
            
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new { Message = "Username and password are required" });
            }

            return Ok(new { Message = "User registered successfully" });
        }

        private bool IsValidUser(LoginModel model)
        {
            return !string.IsNullOrEmpty(model.Username) && 
                   !string.IsNullOrEmpty(model.Password) &&
                   (model.Username == "admin" && model.Password == "password" ||
                    model.Username == "user" && model.Password == "password");
        }

        private User GetUserInfo(string username)
        {
            return new User
            {
                Id = username == "admin" ? 1 : 2,
                Username = username,
                Email = $"{username}@example.com",
                Role = username == "admin" ? "Admin" : "User"
            };
        }

        private string GenerateJwtToken(string username)
        {
            var user = GetUserInfo(username);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
