using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
namespace ManagementService.Controller.v2;

[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]

public class HelloController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult Get()
    {
        return Ok("Hello World! api version=2.0.0");
    }
}