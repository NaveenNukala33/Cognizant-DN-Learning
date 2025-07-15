using Microsoft.AspNetCore.Mvc;

namespace MyCompleteWebAPI.Controllers
{
    /// <summary>
    /// Values Controller - Improved version with proper ID handling
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private static readonly Dictionary<int, string> _values = new()
        {
            { 1, "value1" },
            { 2, "value2" },
            { 3, "value3" }
        };
        private static int _nextId = 4;

        /// <summary>
        /// GET: Get all values
        /// </summary>
        [HttpGet]
        public ActionResult<Dictionary<int, string>> GetAll() => Ok(_values);

        /// <summary>
        /// GET: Get value by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (!_values.ContainsKey(id))
                return NotFound($"Value with ID {id} not found");

            return Ok(_values[id]);
        }

        /// <summary>
        /// POST: Create new value
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Value cannot be empty");

            _values[_nextId] = value;
            int createdId = _nextId;
            _nextId++;

            return CreatedAtAction(nameof(Get), new { id = createdId }, new { id = createdId, value = value });
        }

        /// <summary>
        /// PUT: Update value by ID
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string value)
        {
            if (!_values.ContainsKey(id))
                return NotFound($"Value with ID {id} not found");

            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Value cannot be empty");

            _values[id] = value;
            return Ok(new { message = $"Value {id} updated successfully", data = value });
        }

        /// <summary>
        /// DELETE: Remove value by ID
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_values.ContainsKey(id))
                return NotFound($"Value with ID {id} not found");

            _values.Remove(id);
            return Ok(new { message = $"Value {id} deleted successfully" });
        }

        /// <summary>
        /// GET: Get current status/info
        /// </summary>
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            return Ok(new 
            { 
                totalItems = _values.Count,
                nextId = _nextId,
                availableIds = _values.Keys.OrderBy(k => k).ToList()
            });
        }
    }
}