using Lockshot.User.API.Core.DTOs;
using Lockshot.User.API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lockshot.User.API.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HitsController : ControllerBase
    {
        private readonly IHitService _hitService;

        public HitsController(IHitService hitService)
        {
            _hitService = hitService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetHitsByUser(int userId, [FromQuery] bool sortDescending = false) 
        {
            var hits = await _hitService.GetHitsByUserAsync(userId, sortDescending); 
            return Ok(hits);
        }

        [HttpPost]
        public async Task<IActionResult> SaveHit(HitDto hitDto)
        {
            await _hitService.SaveHitAsync(hitDto);
            return Ok();
        }
    }
}
