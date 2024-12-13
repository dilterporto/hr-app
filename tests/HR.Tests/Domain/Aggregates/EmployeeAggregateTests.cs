using HR.Domain.Aggregates.Employees;
using HR.Domain.Shared.ValueObjects;

namespace HR.Tests.Domain.Aggregates;

public class EmployeeAggregateTests
{
  [Fact]
  public void CreateEmployee()
  {
    // Arrange
    var state = new EmployeeState
    {
      FirstName = "John",
      LastName = "Doe",
      Address = new Address
      {
        Line1 = "123 Elm St",
        City = "Springfield",
        State = "IL",
        ZipCode = "62701"
      },
      PhoneNumber = "555-555",
      HireDate = DateTime.Now,
      DepartmentId = Guid.NewGuid()
    };
    
    var employee = new Employee(state);
    
    // Act
    var actual = employee.State;
    
    // Assert
    Assert.Equal(state.FirstName, actual.FirstName);
    Assert.Equal(state.LastName, actual.LastName);
    Assert.Equal(state.Address, actual.Address);
    Assert.Equal(state.PhoneNumber, actual.PhoneNumber);
    Assert.Equal(state.HireDate, actual.HireDate);
    Assert.Equal(state.DepartmentId, actual.DepartmentId);
  }
  
  [Fact]
  public void GivenAValidState_WhenCreatingAnAggregate_ThenShouldContainOneUncommitedEvent()
  {
    // Arrange
    var state = new EmployeeState
    {
      FirstName = "John",
      LastName = "Doe",
      Address = new Address
      {
        Line1 = "123 Elm St",
        City = "Springfield",
        State = "IL",
        ZipCode = "62701"
      },
      PhoneNumber = "555-555",
      HireDate = DateTime.Now,
      DepartmentId = Guid.NewGuid()
    };
    
    // Act
    var employee = new Employee(state);
    
    // Assert
    Assert.Single(employee.UncommittedEvents);
  }
}

