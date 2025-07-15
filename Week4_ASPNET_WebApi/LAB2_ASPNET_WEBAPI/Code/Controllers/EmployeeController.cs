using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwaggerWebAPIDemo.Data;
using SwaggerWebAPIDemo.Models;

namespace SwaggerWebAPIDemo.Controllers
{
    /// <summary>
    /// Employee API Controller - Demonstrates Route attributes, ActionName, and ProducesResponseType
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EmployeeController(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all employees
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
                    Message = "Employees retrieved successfully",
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
        /// Get employee by ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details</returns>
        [HttpGet("{id}")]
        [ActionName("GetEmployeeById")]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status500InternalServerError)]
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
                        Message = $"Employee with ID {id} not found"
                    });
                }

                return Ok(new ApiResponse<Employee>
                {
                    Success = true,
                    Message = "Employee retrieved successfully",
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

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="employeeDto">Employee creation data</param>
        /// <returns>Created employee</returns>
        [HttpPost]
        [ActionName("CreateEmployee")]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<Employee>>> CreateEmployee(EmployeeCreateDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<Employee>
                    {
                        Success = false,
                        Message = "Invalid model state",
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                    });
                }

                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    Email = employeeDto.Email,
                    Department = employeeDto.Department,
                    Salary = employeeDto.Salary
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, new ApiResponse<Employee>
                {
                    Success = true,
                    Message = "Employee created successfully",
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

        /// <summary>
        /// Update existing employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="employeeDto">Employee update data</param>
        /// <returns>Updated employee</returns>
        [HttpPut("{id}")]
        [ActionName("UpdateEmployee")]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<Employee>>> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound(new ApiResponse<Employee>
                    {
                        Success = false,
                        Message = $"Employee with ID {id} not found"
                    });
                }

                employee.Name = employeeDto.Name;
                employee.Email = employeeDto.Email;
                employee.Department = employeeDto.Department;
                employee.Salary = employeeDto.Salary;
                employee.IsActive = employeeDto.IsActive;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<Employee>
                {
                    Success = true,
                    Message = "Employee updated successfully",
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

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Confirmation message</returns>
        [HttpDelete("{id}")]
        [ActionName("DeleteEmployee")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Employee with ID {id} not found"
                    });
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Employee deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Internal server error",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        /// <summary>
        /// Get employees by department - Demonstrates custom routing
        /// </summary>
        /// <param name="department">Department name</param>
        /// <returns>List of employees in department</returns>
        [HttpGet("department/{department}")]
        [ActionName("GetEmployeesByDepartment")]
        [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> GetByDepartment(string department)
        {
            try
            {
                var employees = await _context.Employees
                    .Where(e => e.Department.ToLower() == department.ToLower())
                    .ToListAsync();

                return Ok(new ApiResponse<List<Employee>>
                {
                    Success = true,
                    Message = $"Employees in {department} department retrieved successfully",
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
        /// Alternative method name for getting all employees - demonstrates multiple methods with same verb
        /// </summary>
        /// <returns>List of all employees</returns>
        [HttpGet("all")]
        [ActionName("GetAllEmployeesAlternative")]
        [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> GetAllEmployeesAlternative()
        {
            return await GetEmployees();
        }
    }
}