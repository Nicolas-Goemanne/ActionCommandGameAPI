using ActionCommandGame.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("{playerId}/actions")]
        public async Task<IActionResult> PerformAction(int playerId)
        {
            var result = await _gameService.PerformAction(playerId);
            return Ok(result);
        }

        [HttpPost("{playerId}/buy/{itemId}")]
        public async Task<IActionResult> BuyItem(int playerId, int itemId)
        {
            var result = await _gameService.Buy(playerId, itemId);
            return Ok(result);
        }
    }
}
