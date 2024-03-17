using System.Net;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Common;
using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.GetCustomers;
using Application.Exceptions;
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
    [ProducesErrorResponseType(typeof(ApiException))]
    public async Task<ActionResult<IList<CustomerResponse>>> GetCustomers()
    {
        var query = new GetCustomersQuery();
        var customers = await _mediator.Send(query);
        return Ok(customers);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ApiException))]
    public async Task<ActionResult<CustomerResponse>> CreateCustomer(CreateCustomerCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }

    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ApiException))]
    public async Task<ActionResult> GetCustomer(Guid customerId)
    {
        var query = new GetCustomerQuery(customerId);
        var customer = await _mediator.Send(query);
        return Ok(customer);
    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ApiException))]
    public async Task<ActionResult> DeleteCustomer(Guid customerId)
    {
        return Ok();
    }

    [HttpPut("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ApiException))]
    public async Task<ActionResult> UpdateCustomer(Guid customerId)
    {
        return Ok();
    }
}
