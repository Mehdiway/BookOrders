using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    [HttpGet]
    public IActionResult Default()
    {
        return Ok("Gateway running...");
    }
}
