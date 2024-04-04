using Application.Common;
using Application.Common.Interfaces.Persistence;
using Application.Customers.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IList<CustomerResponse>>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IList<CustomerResponse>>
{
    private readonly IRepository<Customer> _customerRepository;

    public GetCustomersQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IList<CustomerResponse>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken
    )
    {
        var customers = await _customerRepository.NoTrackingQuery.ToListAsync(
            cancellationToken: cancellationToken
        );

        return customers
            .Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber, 
                BankAccountNumber = customer.BankAccountNumber,
                Email = customer.Email
            })
            .ToList();
    }
}
