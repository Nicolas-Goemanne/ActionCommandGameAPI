using Microsoft.AspNetCore.Mvc;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Results;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NegativeGameEventController : ControllerBase
{
    private readonly INegativeGameEventService _negativeGameEventService;

    public NegativeGameEventController(INegativeGameEventService negativeGameEventService)
    {
        _negativeGameEventService = negativeGameEventService;
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomNegativeGameEvent()
    {
        var result = await _negativeGameEventService.GetRandomNegativeGameEvent();
        if (result.Data == null)
        {
            return NotFound(result.Messages);
        }
        return Ok(result);
    }
}
