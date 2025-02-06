using Lockshot.User.API.Class;
using Lockshot.User.API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lockshot.User.API.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GunsController : ControllerBase
    {
        private readonly IGunService _gunService;

        public GunsController(IGunService gunService)
        {
            _gunService = gunService;
        }

        [HttpGet("{userId}/all")]
        public async Task<IActionResult> GetAllGunsAsync(int userId)
        {
            var guns = await _gunService.GetAllGunsAsync(userId);

            if (guns == null || !guns.Any())
            {
                return NotFound("No guns found.");
            }

            return Ok(guns);
        }

        [HttpGet("{userId}/calibre/{calibre}")]
        public async Task<IActionResult> GetGunsByCalibre(int userId, string calibre)
        {
            var guns = await _gunService.GetMostGunByCalibre(userId, calibre);

            if (guns == null || !guns.Any())
            {
                return NotFound("No guns found for the specified calibre.");
            }

            return Ok(guns);
        }

        [HttpGet("{userId}/name/{nameGun}")]
        public async Task<IActionResult> GetGunsByName(int userId, string nameGun)
        {
            var guns = await _gunService.GetMostGunByNameGun(userId, nameGun);

            if (guns == null || !guns.Any())
            {
                return NotFound("No guns found for the specified name.");
            }

            return Ok(guns);
        }

        [HttpGet("{userId}/id/{id}")]
        public async Task<IActionResult> GetGunById(int userId, int id)
        {
            var guns = await _gunService.GetMostGunById(userId, id);

            if (guns == null || !guns.Any())
            {
                return NotFound("No gun found for the specified ID.");
            }

            return Ok(guns);
        }

        [HttpGet("{userId}/weaponType/{weaponType}")]
        public async Task<IActionResult> GetGunsByWeaponType(int userId, string weaponType)
        {
            var guns = await _gunService.GetMostGunByWeaponType(userId, weaponType);

            if (guns == null || !guns.Any())
            {
                return NotFound("No guns found for the specified weapon type.");
            }

            return Ok(guns);
        }

        [HttpGet("{userId}/firingMode/{firingMode}")]
        public async Task<IActionResult> GetGunsByFiringMode(int userId, string firingMode)
        {
            var guns = await _gunService.GetMostGunByFiringMode(userId, firingMode);

            if (guns == null || !guns.Any())
            {
                return NotFound("No guns found for the specified firing mode.");
            }

            return Ok(guns);
        }
    }
}
