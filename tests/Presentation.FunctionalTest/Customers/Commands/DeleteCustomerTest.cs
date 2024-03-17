using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Queries.GetCustomers;
using Application.Exceptions;
using Domain.Entities;
using Presentation.FunctionalTest.Utils;
using Xunit;

namespace Presentation.FunctionalTest.Customers.Commands;

public class DeleteCustomerTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly MediatorHelper _mediatorHelper;
    private readonly DatabaseHelper _databaseHelper;

    public DeleteCustomerTest(CustomWebApplicationFactory factory)
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
    public async Task DeleteCustomer_Should_ThrowNotFoundException_When_CustomerNotExist()
    {
        var command = new DeleteCustomerCommand(Guid.Empty);
        await Assert.ThrowsAsync<NotFoundException>(() => _mediatorHelper.SendAsync(command));
    }

    [Fact]
    public async Task DeleteCustomer_Should_DeleteCustomer_When_Requested()
    {
        var id = Guid.NewGuid();
        await _databaseHelper.AddAsync(
            new Customer
            {
                Id = id,
                Email = "email@gmail.com",
                FirstName = "First",
                LastName = "Last",
                PhoneNumber = "+989000000000",
                DateOfBirth = DateTimeOffset.UtcNow
            }
        );

        var command = new DeleteCustomerCommand(id);
        await _mediatorHelper.SendAsync(command);
        var customer = await _databaseHelper.FindAsync<Customer>([id]);

        Assert.Null(customer);
    }
}
