using Application.Customers.Queries.GetCustomers;
using Domain.Entities;
using Presentation.FunctionalTest.Utils;
using Xunit;

namespace Presentation.FunctionalTest.Customers.Queries;

public class GetCustomersTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly MediatorHelper _mediatorHelper;
    private readonly DatabaseHelper _databaseHelper;

    public GetCustomersTest(CustomWebApplicationFactory factory)
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
    public async Task GetCustomers_Should_ReturnEmptyCustomers_When_NoCustomerExist()
    {
        var query = new GetCustomersQuery();
        var result = await _mediatorHelper.SendAsync(query);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCustomers_Should_ReturnSingleCustomer_When_ExistOnDatabase()
    {
        var id = Guid.NewGuid();
        await _databaseHelper.AddAsync(
            new Customer
            {
                Id = id,
                Email = "email@gmail.com",
                FirstName = "First",
                LastName = "Last",
                PhoneNumber = 9032338887,
                DateOfBirth = DateTimeOffset.UtcNow
            }
        );

        var query = new GetCustomersQuery();
        var result = await _mediatorHelper.SendAsync(query);

        Assert.Single(result);
        Assert.Equal("First", actual: result.First().FirstName);
        Assert.Equal("Last", actual: result.First().LastName);
        Assert.Equal(id, actual: result.First().Id);
    }
}
