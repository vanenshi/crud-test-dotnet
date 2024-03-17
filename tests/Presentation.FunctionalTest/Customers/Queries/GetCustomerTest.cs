using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.GetCustomers;
using Application.Exceptions;
using Domain.Entities;
using Presentation.FunctionalTest.Utils;
using Xunit;

namespace Presentation.FunctionalTest.Customers.Queries;

public class GetCustomerTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly MediatorHelper _mediatorHelper;
    private readonly DatabaseHelper _databaseHelper;

    public GetCustomerTest(CustomWebApplicationFactory factory)
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
    public async Task GetCustomer_Should_ThrowNotFound_When_NoCustomerExist()
    {
        var query = new GetCustomerQuery(Guid.Empty);
        await Assert.ThrowsAsync<NotFoundException>(() => _mediatorHelper.SendAsync(query));
    }

    [Fact]
    public async Task GetCustomer_Should_ReturnSingleCustomer_When_ExistOnDatabase()
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

        var query = new GetCustomerQuery(id);
        var result = await _mediatorHelper.SendAsync(query);

        Assert.Equal("First", actual: result.FirstName);
        Assert.Equal("Last", actual: result.LastName);
        Assert.Equal(id, actual: result.Id);
    }
}
