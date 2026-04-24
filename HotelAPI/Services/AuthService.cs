using HotelAPI.Data;
using HotelAPI.DTOs.Auth;
using HotelAPI.Helpers;
using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelAPI.Services // Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher _hasher;
        private readonly JwtSettings _jwt;

        public AuthService(AppDbContext context, PasswordHasher hasher, JwtSettings jwt)
        {
            _context = context;
            _hasher = hasher;
            _jwt = jwt;
        }

        // ----------------------
        // REGISTER
        // ----------------------
        public async Task<RegisterResponseDto> Register(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = _hasher.HashPassword(dto.Password),
                Role = "Customer"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            };
        }

        // ----------------------
        // LOGIN
        // ----------------------
        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || !_hasher.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            if (!user.IsActive)
                throw new Exception("User is deactivated");

            return GenerateResponse(user);
        }

        // ----------------------
        // TOKEN GENERATION
        // ----------------------
        private AuthResponseDto GenerateResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwt.Key);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                },
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}