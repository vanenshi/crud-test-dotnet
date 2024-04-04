using Application.Common.Interfaces.Persistence;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid CustomerId) : IRequest;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IRepository<Customer> _customerRepository;

    public DeleteCustomerCommandHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindAsync(
            [request.CustomerId],
            cancellationToken: cancellationToken
        );

        if (customer == null)
            throw new NotFoundException("customers", request.CustomerId);

        await _customerRepository.DeleteAsync(customer, cancellationToken);
    }
}
