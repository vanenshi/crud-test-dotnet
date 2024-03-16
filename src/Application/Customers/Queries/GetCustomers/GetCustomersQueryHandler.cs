using Application.Customers.Common;
using MediatR;

namespace Application.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IList<CustomerResponse>>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IList<CustomerResponse>>
{
    public async Task<IList<CustomerResponse>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken
    )
    {
        IList<CustomerResponse> customers = new List<CustomerResponse>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Dummy",
                LastName = "User"
            }
        };
        
        return customers;
    }
}