namespace Application.Customers.Common;

public class CustomerResponse
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}