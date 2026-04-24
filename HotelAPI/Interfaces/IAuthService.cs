using HotelAPI.DTOs.Auth;

namespace HotelAPI.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}