using Microsoft.AspNetCore.Mvc;

namespace SwaggerWebAPIDemo.Controllers
{
    /// <summary>
    /// Values Controller - Default controller for demonstration
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _values = new() { "value1", "value2", "value3" };

        /// <summary>
        /// GET: Get all values
        /// </summary>
        /// <returns>List of values</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_values);
        }

        /// <summary>
        /// GET: Get value by ID
        /// </summary>
        /// <param name="id">Value ID</param>
        /// <returns>Single value</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Get(int id)
        {
            if (id < 0 || id >= _values.Count)
                return NotFound("Value with ID {id} not found");

            return Ok(_values[id]);
        }

        /// <summary>
        /// POST: Create new value
        /// </summary>
        /// <param name="value">Value to create</param>
        /// <returns>Created value</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Value cannot be empty");

            _values.Add(value);
            return CreatedAtAction(nameof(Get), new { id = _values.Count - 1 }, value);
        }

        /// <summary>
        /// PUT: Update value by ID
        /// </summary>
        /// <param name="id">Value ID</param>
        /// <param name="value">New value</param>
        /// <returns>Update result</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0 || id >= _values.Count)
                return NotFound("Value with ID {id} not found");

            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Value cannot be empty");

            _values[id] = value;
            return Ok(new { message = "Value {id} updated successfully", data = value });
        }

        /// <summary>
        /// DELETE: Remove value by ID
        /// </summary>
        /// <param name="id">Value ID</param>
        /// <returns>Delete result</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id < 0 || id >= _values.Count)
                return NotFound("Value with ID {id} not found");

            _values.RemoveAt(id);
            return Ok(new { message = "Value {id} deleted successfully" });
        }
    }
}
