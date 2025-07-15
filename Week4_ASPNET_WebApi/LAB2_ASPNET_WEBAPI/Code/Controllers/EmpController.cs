using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwaggerWebAPIDemo.Data;
using SwaggerWebAPIDemo.Models;

namespace SwaggerWebAPIDemo.Controllers
{
    /// <summary>
    /// Employee API Controller with modified route 'Emp' - Demonstrates Route attribute modification
    /// </summary>
    [ApiController]
    [Route("api/Emp")]
    public class EmpController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EmpController(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all employees via /api/Emp
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        [ActionName("GetAllEmployees")]
        [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                
                return Ok(new ApiResponse<List<Employee>>
                {
                    Success = true,
                    Message = "Employees retrieved successfully via /api/Emp",
                    Data = employees
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<Employee>>
                {
                    Success = false,
                    Message = "Internal server error",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get employee by ID via /api/Emp/{id}
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details</returns>
        [HttpGet("{id}")]
        [ActionName("GetEmployeeById")]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<Employee>>> GetEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound(new ApiResponse<Employee>
                    {
                        Success = false,
                        Message = "Employee with ID {id} not found"
                    });
                }

                return Ok(new ApiResponse<Employee>
                {
                    Success = true,
                    Message = "Employee retrieved successfully via /api/Emp",
                    Data = employee
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<Employee>
                {
                    Success = false,
                    Message = "Internal server error",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
