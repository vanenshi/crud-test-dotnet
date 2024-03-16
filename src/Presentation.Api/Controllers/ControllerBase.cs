using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase { }