using Microsoft.AspNetCore.Mvc;
using ActionCommandGame.Services;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Filters;


[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet("{playerId}")]
    public async Task<IActionResult> GetPlayer(int playerId)
    {
        var result = await _playerService.Get(playerId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlayers([FromQuery] PlayerFilter? filter)
    {
        var result = await _playerService.Find(filter);
        return Ok(result);
    }
}
