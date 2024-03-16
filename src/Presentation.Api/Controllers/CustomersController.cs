using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[Route("customers")]
public class CustomersController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCustomers()
    {
        return Ok();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateCustomer()
    {
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
