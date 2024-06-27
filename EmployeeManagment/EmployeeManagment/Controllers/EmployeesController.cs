using EmployeeManagment.Filters;
using EmployeeManagment.Models;
using EmployeeManagment.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    [HttpGet]
    [Route("")]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    [ServiceFilter(typeof(ResourseFilter))]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllAsync()
    {
        if (!(await _employeeRepository.GetAllAsync()).Any())
        {
            await _employeeRepository.AddEmployeeAsync(new Employee
            {
                FirstName = "Taras",
                LastName = "Shevchenko",
                Email = "123@gmail.com",
                Phone = "1",
                Position = "Writer/Drawer"
            });
        }
        
        return Ok(await _employeeRepository.GetAllAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetByIdAsync([FromRoute] int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee == null ? NotFound() : Ok(employee);
    }
    
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<ActionResult<Employee>> CreateEmployeeAsync(Employee employee)
    {
        await _employeeRepository.AddEmployeeAsync(employee);
        return CreatedAtAction(employee.Id.ToString(), employee);
    }
    
    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id, Employee employee)
        {
        if (id != employee.Id)
        {
            return BadRequest();
        }
        
        await _employeeRepository.UpdateEmployeeAsync(employee);
        return Ok(employee);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int id)
    {
        try
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}