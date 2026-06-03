using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ManagementService.Controller.v1;

[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]

public class HelloController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult Get()
    {
        return Ok("Hello World! api version=1.0.0");
    }
}