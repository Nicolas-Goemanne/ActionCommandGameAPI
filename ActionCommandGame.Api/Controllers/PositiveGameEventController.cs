using Microsoft.AspNetCore.Mvc;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Results;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PositiveGameEventController : ControllerBase
{
    private readonly IPositiveGameEventService _positiveGameEventService;

    public PositiveGameEventController(IPositiveGameEventService positiveGameEventService)
    {
        _positiveGameEventService = positiveGameEventService;
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomPositiveGameEvent([FromQuery] bool hasAttackItem)
    {
        var result = await _positiveGameEventService.GetRandomPositiveGameEvent(hasAttackItem);
        if (result.Data == null)
        {
            return NotFound(result.Messages);
        }
        return Ok(result);
    }
}
