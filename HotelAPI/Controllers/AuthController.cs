using Microsoft.AspNetCore.Mvc;
using HotelAPI.Interfaces;
using HotelAPI.DTOs.Auth;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ----------------------
        // REGISTER
        // ----------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);
            return Ok(result);
        }

        // ----------------------
        // LOGIN
        // ----------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.Login(dto);
            return Ok(result);
        }
    }
}