using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity<Guid>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTimeOffset DateOfBirth { get; set; }

    public string? BankAccountNumber { get; set; }

    public ulong PhoneNumber { get; set; }

    public string Email { get; set; } = null!;
}