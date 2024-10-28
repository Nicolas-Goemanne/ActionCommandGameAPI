using Microsoft.AspNetCore.Mvc;
using ActionCommandGame.Services;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Abstractions;

[ApiController]
[Route("api/[controller]")]
public class PlayerItemController : ControllerBase
{
    private readonly IPlayerItemService _playerItemService;

    public PlayerItemController(IPlayerItemService playerItemService)
    {
        _playerItemService = playerItemService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerItem(int id)
    {
        var result = await _playerItemService.Get(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlayerItems([FromQuery] PlayerItemFilter filter)
    {
        var result = await _playerItemService.Find(filter);
        return Ok(result);
    }

    [HttpPost("{playerId}/{itemId}")]
    public async Task<IActionResult> CreatePlayerItem(int playerId, int itemId)
    {
        var result = await _playerItemService.Create(playerId, itemId);
        if (result.Data == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetPlayerItem), new { id = result.Data.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayerItem(int id)
    {
        await _playerItemService.Delete(id);
        return NoContent();
    }
}


