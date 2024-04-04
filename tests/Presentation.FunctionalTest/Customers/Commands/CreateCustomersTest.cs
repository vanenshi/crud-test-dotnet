using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomers;
using Application.Exceptions;
using Domain.Entities;
using Presentation.FunctionalTest.Utils;
using Xunit;

namespace Presentation.FunctionalTest.Customers.Commands;

public class CreateCustomersTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly MediatorHelper _mediatorHelper;
    private readonly DatabaseHelper _databaseHelper;

    public CreateCustomersTest(CustomWebApplicationFactory factory)
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
    public async Task CreateCustomer_Should_ThrowValidationError_When_RequestedWithoutData()
    {
        var command = new CreateCustomerCommand();
        await Assert.ThrowsAsync<ArgValidationException>(() => _mediatorHelper.SendAsync(command));
    }

    [Fact]
    public async Task CreateCustomer_Should_CreateCustomer_When_Requested()
    {
        var command = new CreateCustomerCommand
        {
            Email = "email@gmail.com",
            FirstName = "First",
            LastName = "Last",
            PhoneNumber = "+989000000000",
            DateOfBirth = DateTimeOffset.UtcNow
        };

        var id = await _mediatorHelper.SendAsync(command);
        var customer = await _databaseHelper.FindAsync<Customer>([id]);

        Assert.NotNull(customer);
        Assert.Equal("First", actual: customer!.FirstName);
        Assert.Equal("Last", actual: customer.LastName);
        Assert.Equal(id, actual: customer.Id);
    }
}
