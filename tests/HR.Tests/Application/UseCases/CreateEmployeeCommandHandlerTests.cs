using AutoFixture;
using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Application.Contracts;
using HR.Application.UseCases.CreateEmployee;
using HR.Domain.Aggregates.Employees;
using Moq;

namespace HR.Tests.Application.UseCases;

public class CreateEmployeeCommandHandlerTests
{
  private readonly CreateEmployeeCommandHandler _handler;
  
  public CreateEmployeeCommandHandlerTests()
  {
    var repository = new Mock<IEmployeeRepository>();
    var mapper = new Mock<IMapper>();
    var employeeFactory = new Mock<IEmployeeFactory>();
    
    _handler = new CreateEmployeeCommandHandler(repository.Object, mapper.Object, employeeFactory.Object);
  }
  
  [Fact]
  public async Task HandleAsync_WhenCalled_ShouldCreateEmployee()
  {
    // Arrange
    var command = new Fixture().Create<CreateEmployeeCommandHandler.Command>();
    var employee = new Employee(new EmployeeState());
    var employeeResponse = new EmployeeResponse();
    var cancellationToken = CancellationToken.None;
    
    var mapper = new Mock<IMapper>();
    mapper.Setup(x => x.Map<EmployeeState>(command)).Returns(new EmployeeState());
    mapper.Setup(x => x.Map<EmployeeResponse>(employee.State)).Returns(employeeResponse);
    
    var repository = new Mock<IEmployeeRepository>();
    repository
      .Setup(x => x.SaveAsync(It.IsAny<Employee>()))
      .ReturnsAsync(Result.Success());
    
    var employeeFactory = new Mock<IEmployeeFactory>();
    employeeFactory.Setup(x => x.CreateAsync(It.IsAny<EmployeeState>())).ReturnsAsync(employee);
    
    var handler = new CreateEmployeeCommandHandler(repository.Object, mapper.Object, employeeFactory.Object);
    
    // Act
    var actual = await handler.Handle(command, cancellationToken);
    
    // Assert
    Assert.True(actual.IsSuccess);
    Assert.Equal(employeeResponse, actual.Value);
  }
  
  [Fact]
  public async Task HandleAsync_WhenEmployeeFactoryFails_ShouldReturnFailure()
  {
    // Arrange
    var command = new Fixture().Create<CreateEmployeeCommandHandler.Command>();
    var cancellationToken = CancellationToken.None;
    
    var mapper = new Mock<IMapper>();
    mapper.Setup(x => x.Map<EmployeeState>(command)).Returns(new EmployeeState());
    
    var repository = new Mock<IEmployeeRepository>();
    
    var employeeFactory = new Mock<IEmployeeFactory>();
    employeeFactory
      .Setup(x => x.CreateAsync(It.IsAny<EmployeeState>()))
      .ReturnsAsync(Maybe<Employee>.None);
    
    var handler = new CreateEmployeeCommandHandler(repository.Object, mapper.Object, employeeFactory.Object);
    
    // Act
    var actual = await handler.Handle(command, cancellationToken);
    
    // Assert
    Assert.True(actual.IsFailure);
    Assert.Equal("Error creating employee", actual.Error);
  }
  
  [Fact]
  public async Task HandleAsync_WhenRepositorySaveFails_ShouldReturnFailure()
  {
    // Arrange
    var command = new Fixture().Create<CreateEmployeeCommandHandler.Command>();
    var employee = new Employee(new EmployeeState());
    var cancellationToken = CancellationToken.None;
    
    var mapper = new Mock<IMapper>();
    mapper.Setup(x => x.Map<EmployeeState>(command)).Returns(new EmployeeState());
    
    var repository = new Mock<IEmployeeRepository>();
    repository
      .Setup(x => x.SaveAsync(It.IsAny<Employee>()))
      .ReturnsAsync(Result.Failure("Error saving employee"));
    
    var employeeFactory = new Mock<IEmployeeFactory>();
    employeeFactory.Setup(x => x.CreateAsync(It.IsAny<EmployeeState>())).ReturnsAsync(employee);
    
    var handler = new CreateEmployeeCommandHandler(repository.Object, mapper.Object, employeeFactory.Object);
    
    // Act
    var actual = await handler.Handle(command, cancellationToken);
    
    // Assert
    Assert.True(actual.IsFailure);
    Assert.Equal("Error saving employee", actual.Error);
  }
}
