using Application.Common.Interfaces.Persistence;
using Application.Common.Validators;
using Application.Customers.Commands.DeleteCustomer;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? BankAccountNumber { get; set; }
    public string Email { get; set; } = null!;
}

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.DateOfBirth).LessThan(DateTimeOffset.Now);
        RuleFor(x => x.PhoneNumber).PhoneNumber();
        RuleFor(x => x.Email).EmailAddress();
    }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IRepository<Customer> _customerRepository;

    public UpdateCustomerCommandHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindAsync(
            [request.Id],
            cancellationToken: cancellationToken
        );

        if (customer == null)
            throw new NotFoundException("customers", request.Id);

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.DateOfBirth = request.DateOfBirth;
        customer.Email = request.Email;
        customer.PhoneNumber = request.PhoneNumber;
        customer.BankAccountNumber = request.BankAccountNumber;

        await _customerRepository.UpdateAsync(
            entity: customer,
            cancellationToken: cancellationToken
        );
    }
}
