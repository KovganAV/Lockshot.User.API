using Lockshot.User.API.Core.DTOs;
using Lockshot.User.API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lockshot.User.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HitsController : ControllerBase
    {
        private readonly IHitService _hitService;

        public HitsController(IHitService hitService)
        {
            _hitService = hitService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveHit([FromBody] HitDto hitDto)
        {
            await _hitService.SaveHitAsync(hitDto);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetHitsByUser(int userId)
        {
            var hits = await _hitService.GetHitsByUserAsync(userId);
            return Ok(hits);
        }
    }
}
