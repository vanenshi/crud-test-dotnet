using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity<Guid>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTimeOffset DateOfBirth { get; set; }

    public string? BankAccountNumber { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;
}
