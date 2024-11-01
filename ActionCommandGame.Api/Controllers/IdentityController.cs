using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;
using Microsoft.AspNetCore.Mvc;
using ActionCommandGame.Services;
using Microsoft.AspNetCore.Identity.Data;

namespace ActionCommandGame.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController(IdentityService identityService) : ControllerBase
    {
        private readonly IdentityService _identityService = identityService;

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserSignInRequest request)
        {
            var result = await _identityService.SignIn(request);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var result = await _identityService.Register(request);
            return Ok(result);
        }
    }
}

