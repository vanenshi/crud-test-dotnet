using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Common;
using Application.Customers.Queries.GetCustomers;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Persistence;
using Presentation.FunctionalTest.Utils;
using Xunit;

namespace Presentation.FunctionalTest.Customers.Commands;

public class UpdateCustomerTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly MediatorHelper _mediatorHelper;
    private readonly DatabaseHelper _databaseHelper;

    public UpdateCustomerTest(CustomWebApplicationFactory factory)
    {
        _mediatorHelper = new MediatorHelper(factory);
        _databaseHelper = new DatabaseHelper(factory);
    }

    public async Task InitializeAsync()
    {
        await _databaseHelper.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await _databaseHelper.DisposeAsync();
    }

    [Fact]
    public async Task UpdateCustomer_Should_ThrowValidationError_When_RequestedWithoutData()
    {
        var command = new UpdateCustomerCommand();
        await Assert.ThrowsAsync<ArgValidationException>(() => _mediatorHelper.SendAsync(command));
    }

    [Fact]
    public async Task UpdateCustomer_Should_UpdateCustomer_When_Requested()
    {
        var id = Guid.NewGuid();
        var birthDate = DateTimeOffset.UtcNow;
        var customerEntity = new Customer
        {
            Id = id,
            Email = "email@gmail.com",
            FirstName = "First",
            LastName = "Last",
            PhoneNumber = "+989000000000",
            DateOfBirth = birthDate
        };

        await _databaseHelper.AddAsync(customerEntity);

        var command = new UpdateCustomerCommand
        {
            Id = id,
            Email = "email1@gmail.com",
            FirstName = "First1",
            LastName = "Last1",
            PhoneNumber = "+989000000001",
            DateOfBirth = birthDate - TimeSpan.FromDays(7)
        };

        await _mediatorHelper.SendAsync(command);
        var customer = await _databaseHelper.FindAsync<Customer>([id]);

        Assert.NotNull(customer);
        Assert.Equal("First1", actual: customer!.FirstName);
        Assert.Equal("Last1", actual: customer.LastName);
        Assert.Equal("+989000000001", actual: customer.PhoneNumber);
        Assert.Equal(birthDate - TimeSpan.FromDays(7), actual: customer.DateOfBirth);
        Assert.Equal("email1@gmail.com", actual: customer.Email);
    }
}
