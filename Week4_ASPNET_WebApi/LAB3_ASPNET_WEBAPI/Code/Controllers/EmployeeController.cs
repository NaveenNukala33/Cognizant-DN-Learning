using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Lab3_CustomWebApi.Models;
using Lab3_CustomWebApi.Filters;

namespace Lab3_CustomWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomAuthFilter))]
    public class EmployeeController : ControllerBase
    {
        private List<Employee> _employees;

        public EmployeeController()
        {
            _employees = GetStandardEmployeeList();
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John",
                    Salary = 50000,
                    Permanent = true,
                    Department = new Department { Id = 1, DeptName = "HR" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 1, SkillName = "C#" },
                        new Skill { Id = 2, SkillName = "SQL" }
                    },
                    DateOfBirth = new DateTime(1990, 5, 1)
                }
            };
        }

        // GET: api/employee
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<Employee>> Get()
        {
            return Ok(_employees); // return dummy employee list
        }


        // POST: api/employee
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Post([FromBody] Employee employee)
        {
            // Return the posted employee object as-is
            return Ok(employee);
        }
    }
}