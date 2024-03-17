using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Common;
using Application.Customers.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[Route("customers")]
public class CustomersController : ApiControllerBase
{
    private readonly ISender _mediator;

    public CustomersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<CustomerResponse>>> GetCustomers()
    {
        var command = new GetCustomersQuery();
        var customers = await _mediator.Send(command);
        return Ok(customers);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CustomerResponse>> CreateCustomer(CreateCustomerCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }

    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCustomer(Guid customerId)
    {
        return Ok();
    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteCustomer(Guid customerId)
    {
        return Ok();
    }

    [HttpPut("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateCustomer(Guid customerId)
    {
        return Ok();
    }
}
