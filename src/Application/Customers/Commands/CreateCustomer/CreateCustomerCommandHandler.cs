using Application.Common;
using Application.Common.Interfaces.Persistence;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? BankAccountNumber { get; set; }
    public string Email { get; set; } = null!;
}

public class CreateMovieCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.DateOfBirth).LessThan(DateTimeOffset.Now);
        RuleFor(x => x.PhoneNumber).PhoneNumber();
        RuleFor(x => x.Email).EmailAddress();
    }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IRepository<Customer> _customerRepository;

    public CreateCustomerCommandHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken
    )
    {
        var id = Guid.NewGuid();

        var customerEntity = new Customer
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            BankAccountNumber = request.BankAccountNumber
        };

        await _customerRepository.AddAsync(
            entity: customerEntity,
            cancellationToken: cancellationToken
        );

        return id;
    }
}
