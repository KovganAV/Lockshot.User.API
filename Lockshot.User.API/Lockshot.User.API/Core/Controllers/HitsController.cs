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

        [HttpPost]
        public async Task<IActionResult> SaveHit(HitDto hitDto)
        {
            await _hitService.SaveHitAsync(hitDto);
            return Ok();
        }

        [HttpGet("{userId}/{distance}")]
        public async Task<IActionResult> GetMostHits(int userId, double distance)
        {
            var hits = await _hitService.GetMostHits(userId, distance);

            if (hits == null || !hits.Any())
            {
                return NotFound("No hits found for the specified user and distance.");
            }

            return Ok(hits);
        }

        [HttpGet("{userId}/{Metrics}/metrics")]

        public async Task<IActionResult> GetMostHitsByMetrics(int userId, double Metrics)
        {
            var hits = await _hitService.GetMostHitsByMetrics(userId, Metrics);

            if (hits == null || !hits.Any())
            {
                return NotFound("No hits found for the specified user and Metrics.");
            }

            return Ok(hits);
        }

        [HttpGet("{userId}/{Score}/Score")]

        public async Task<IActionResult> GetMostHitsScore(int userId, double Score)
        {
            var hits = await _hitService.GetMostHitsScore(userId, Score);

            if (hits == null || !hits.Any())
            {
                return NotFound("No hits found for the specified user and Metrics.");
            }

            return Ok(hits);
        }
    }
}
