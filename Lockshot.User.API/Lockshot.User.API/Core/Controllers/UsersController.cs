using Lockshot.User.API.Class;
using Lockshot.User.API.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lockshot.User.API.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userService.RegisterAsync(registerUserDto);
            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            try
            {
                var token = await _userService.LoginAsync(loginUserDto);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password.");
            }
        }


        [HttpPost("upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromBody] PhotoUploadDto photoUploadDto)
        {
            var result = await _userService.UpdateUserPhotoAsync(photoUploadDto.UserId, photoUploadDto.PhotoUrl);
            if (!result)
            {
                return NotFound("Пользователь не найден.");
            }

            return Ok("Фото успешно сохранено.");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization header missing or invalid.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var userIdClaim = await _userService.ValidateTokenAndGetUserId(token);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("Invalid token.");
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

    }

    //[HttpGet("{userId}/photo-url")]
    //public async Task<IActionResult> GetPhotoUrl(int userId)
    //{
    //    var user = await _context.Users.FindAsync(userId);
    //    if (user == null || string.IsNullOrEmpty(user.PhotoUrl))
    //    {
    //        return NotFound("Фото не найдено.");
    //    }

    //    return Ok(new { PhotoUrl = user.PhotoUrl });
    //}
}

