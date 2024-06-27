using EmployeeManagment.Controllers;
using EmployeeManagment.Models;
using EmployeeManagment.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebAPI.Testing;

public class EmployeesControllerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepo;
    private readonly EmployeesController _controller;
    
    public EmployeesControllerTests()
    {
        _mockRepo = new Mock<IEmployeeRepository>();
        _controller = new EmployeesController(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCorrectEmployeeList()
    {
        var employeeList = new List<Employee>()
        {
            new Employee { Id = 1, FirstName = "Taras", LastName = "Shevchenko", Email = "taras", Phone = "1", Position = "Writer/Drawer" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
            
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employeeList);
        var resultList = await _mockRepo.Object.GetAllAsync();
        
        Assert.Equal(employeeList, resultList);
        _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        _mockRepo.VerifyNoOtherCalls();
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsOkResult()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Employee>());
    
        var result = await _controller.GetAllAsync();
    
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
    }
    
    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsEmployee()
    {
        var employee = new Employee { Id = 1, FirstName = "Taras", LastName = "Shevchenko", Email = "taras", Phone = "1", Position = "Writer/Drawer" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);
        
        var result = await _controller.GetByIdAsync(1);
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<Employee>(okResult.Value);
        
        Assert.Equal(employee.Id, model.Id);
        Assert.Equal(employee.FirstName, model.FirstName);
        Assert.Equal(employee.LastName, model.LastName);
        Assert.Equal(employee.Email, model.Email);
        Assert.Equal(employee.Phone, model.Phone);
    }
    
    [Fact]
    public async Task GetByIdAsync_IdOutOfRange_ReturnsNotFound()
    {
        var employee = new Employee { Id = 1, FirstName = "Taras", LastName = "Shevchenko", Email = "taras", Phone = "1", Position = "Writer/Drawer" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);
        
        var result = await _controller.GetByIdAsync(2);
        
        Assert.IsType<NotFoundResult>(result.Result);
    }
    
    [Fact]
    public async Task CreateEmployeeAsync_ValidObject_ReturnsCreatedResponse()
    {
        var employee = new Employee { Id = 1, FirstName = "Taras", LastName = "Shevchenko", Email = "taras", Phone = "1", Position = "Writer/Drawer" };
        
        var result = await _controller.CreateEmployeeAsync(employee);
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsAssignableFrom<Employee>(createdResult.Value);
        
        Assert.Equal(employee.Id, model.Id);
        Assert.Equal(employee.FirstName, model.FirstName);
        Assert.Equal(employee.LastName, model.LastName);
        Assert.Equal(employee.Email, model.Email);
        Assert.Equal(employee.Phone, model.Phone);
    }
    
    [Fact]
    public async Task UpdateEmployeeAsync_ValidObject_ReturnsOkResult()
    {
        var employee = new Employee { Id = 1, FirstName = "Taras", LastName = "Shevchenko", Email = "taras", Phone = "1", Position = "Writer/Drawer" };
        
        var result = await _controller.UpdateEmployeeAsync(1, employee);
        
        Assert.IsType<OkObjectResult>(result);
        _mockRepo.Verify(repo => repo.UpdateEmployeeAsync(employee), Times.Once);
    }
    
    [Fact]
    public async Task DeleteEmployeeAsync_ValidId_ReturnsOkResult()
    {
        var employee = new Employee { Id = 1, FirstName = "Taras", LastName = "Shevchenko", Email = "taras", Phone = "1", Position = "Writer/Drawer" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);
        
        var result = await _controller.DeleteEmployeeAsync(1);
        
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteEmployeeAsync(1), Times.Once);
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    public async Task DeleteEmployeeAsync_IdOutOfRange_ReturnsNotFound(int id)
    {
        _mockRepo.Setup(repo => repo.DeleteEmployeeAsync(id)).Throws<Exception>();

        var result = await _controller.DeleteEmployeeAsync(id);
        
        Assert.IsType<NotFoundResult>(result);
        _mockRepo.Verify(repo => repo.DeleteEmployeeAsync(id), Times.Once);
    }
}