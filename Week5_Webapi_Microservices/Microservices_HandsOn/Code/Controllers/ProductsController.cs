using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            
            var products = new[]
            {
                new { Id = 1, Name = "Laptop", Price = 999.99 },
                new { Id = 2, Name = "Mouse", Price = 29.99 },
                new { Id = 3, Name = "Keyboard", Price = 79.99 }
            };

            return Ok(new 
            { 
                Message = $"Hello {username} ({role}), here are the products:",
                Products = products 
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            
            return Ok(new 
            { 
                Id = id,
                Name = $"Sample Product {id}",
                Price = 99.99 + id,
                RequestedBy = username
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct([FromBody] object product)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            
            return Ok(new 
            { 
                Message = $"Product created successfully by {username}",
                Product = product,
                CreatedAt = DateTime.UtcNow
            });
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult GetPublicData()
        {
            return Ok(new 
            { 
                Message = "This is public data, no authentication required!",
                Timestamp = DateTime.UtcNow,
                PublicInfo = "Anyone can access this endpoint"
            });
        }
    }
}
