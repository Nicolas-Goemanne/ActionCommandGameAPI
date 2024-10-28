using Microsoft.AspNetCore.Mvc;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var result = await _itemService.Get(id);
        if (result.Data == null)
        {
            return NotFound(result.Messages);
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItems()
    {
        var result = await _itemService.Find();
        return Ok(result);
    }
}
