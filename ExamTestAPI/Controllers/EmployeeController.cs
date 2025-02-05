using ExamTestAPI.DTO;
using ExamTestAPI.Services.Interfaces;
using ExamTestAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ExamTestAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, CreateErrorMessage(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                    return NotFound(CreateErrorMessage("Employee not found"));
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, CreateErrorMessage(ex.Message));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                EmployeeDtoValidator validator = new EmployeeDtoValidator();

                var result = validator.Validate(employee);
                if (!validator.Validate(employee).IsValid)
                    return BadRequest(CreateErrorMessage(result.Errors.First().ErrorMessage));

                var newEmployee = await _employeeService.AddEmployee(employee);
                if (newEmployee == null)
                    return BadRequest(CreateErrorMessage("Employee not created"));
                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, CreateErrorMessage(ex.Message));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                EmployeeDtoValidator validator = new EmployeeDtoValidator();

                var result = validator.Validate(employee);
                if (!validator.Validate(employee).IsValid)
                    return BadRequest(CreateErrorMessage(result.Errors.First().ErrorMessage));

                var updated = await _employeeService.UpdateEmployee(employee);
                if (!updated)
                    return BadRequest(CreateErrorMessage("Employee not updated"));
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, CreateErrorMessage(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var deleted = await _employeeService.DeleteEmployee(id);
                if (!deleted)
                    return NotFound(CreateErrorMessage("Employee not found"));
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, CreateErrorMessage(ex.Message));
            }
        }

        private string CreateErrorMessage(string message)
        {
            return $"{{ Error: {message} }}";
        }
    }
}
