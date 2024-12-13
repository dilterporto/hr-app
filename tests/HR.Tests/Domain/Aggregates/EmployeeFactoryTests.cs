using AutoFixture;
using CSharpFunctionalExtensions;
using HR.Application.UseCases.CreateEmployee.Factories;
using HR.Domain.Aggregates.Departments;
using HR.Domain.Aggregates.Employees;
using Moq;

namespace HR.Tests.Domain.Aggregates;

public class EmployeeFactoryTests
{
  private readonly IEmployeeFactory _employeeFactory;
  private readonly Mock<IDepartmentRepository> _departmentRepository;

  public EmployeeFactoryTests()
  {
    _departmentRepository = new Mock<IDepartmentRepository>();
    _employeeFactory = new EmployeeFactory(_departmentRepository.Object);
  }
  
  [Fact]
  public async Task CreateAsync_WithValidData_ShouldReturnEmployee()
  {
    // Arrange
    var department = new DepartmentState() { Name = "Department 1", };
    
    _departmentRepository
      .Setup(x => x.LoadByIdAsync(It.IsAny<Guid>()))
      .ReturnsAsync(new Department(department));
    
    var employeeState = new Fixture().Create<EmployeeState>();
    
    // Act
    var result = await _employeeFactory.CreateAsync(employeeState);
    
    // Assert
    Assert.True(result.HasValue);
    Assert.Equal(employeeState.FirstName, result.Value.State.FirstName);
    Assert.Equal(employeeState.LastName, result.Value.State.LastName);
    Assert.Equal(employeeState.HireDate, result.Value.State.HireDate);
    Assert.Equal(employeeState.DepartmentId, result.Value.State.DepartmentId);
    Assert.Equal(employeeState.PhoneNumber, result.Value.State.PhoneNumber);
    Assert.Equal(employeeState.Address, result.Value.State.Address);
  }
  
  [Fact]
  public async Task CreateAsync_WithInvalidDepartmentId_ShouldReturnNone()
  {
    // Arrange
    _departmentRepository
      .Setup(x => x.LoadByIdAsync(It.IsAny<Guid>()))
      .ReturnsAsync(Maybe<Department>.None);
    
    var employeeState = new Fixture().Create<EmployeeState>();
    
    // Act
    var result = await _employeeFactory.CreateAsync(employeeState);
    
    // Assert
    Assert.True(result.HasNoValue);
  }
}
