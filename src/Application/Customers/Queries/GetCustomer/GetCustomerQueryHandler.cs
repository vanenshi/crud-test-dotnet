using Application.Common.Interfaces.Persistence;
using Application.Customers.Common;
using Application.Customers.Queries.GetCustomers;
using Application.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomer;

public record GetCustomerQuery(Guid customerId) : IRequest<CustomerResponse>;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
{
    private readonly IRepository<Customer> _customerRepository;

    public GetCustomerQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerResponse> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken
    )
    {
        var customer = await _customerRepository.FindAsync(
            [request.customerId],
            cancellationToken: cancellationToken
        );

        if (customer == null)
            throw new NotFoundException("customers", request.customerId);

        return new CustomerResponse
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateOfBirth = customer.DateOfBirth,
            PhoneNumber = customer.PhoneNumber,
            BankAccountNumber = customer.BankAccountNumber,
            Email = customer.Email
        };
    }
}
