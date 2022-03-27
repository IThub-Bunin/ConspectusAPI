using Microsoft.AspNetCore.Mvc;

namespace ConspectusAPI.Controllers;

[ApiController]
[Route("api")]
public class SimpleApi : ControllerBase
{
    private readonly ILogger<SimpleApi> _logger;

    public SimpleApi(ILogger<SimpleApi> logger)
    {
        _logger = logger;
    }

    [HttpGet("isitworks_qm")]
    public string Get()
    {
        return "Hello! Conspectus api is works!";
    }
}
