using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using dotnet_project.Data.Repositories;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Auth;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto)
    {
        _logger.LogInformation("Login denemesi: {Email}", dto.Email);
        
        // Email ile
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
        {
            _logger.LogWarning("Başarısız giriş - kullanıcı bulunamadı: {Email}", dto.Email);
            return ApiResponse<LoginResponseDto>.ErrorResponse("Email veya şifre hatalı");
        }

        // Şifre kontrolü
        if (user.PasswordHash != dto.Password)
        {
            _logger.LogWarning("Başarısız giriş - hatalı şifre: {Email}", dto.Email);
            return ApiResponse<LoginResponseDto>.ErrorResponse("Email veya şifre hatalı");
        }

        // JWT token
        var token = GenerateJwtToken(user.Id, user.Email!, user.Role);
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"] ?? "60");

        var response = new LoginResponseDto
        {
            Token = token,
            Email = user.Email!,
            FullName = $"{user.FirstName} {user.LastName}",
            Role = user.Role,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes)
        };

        _logger.LogInformation("Başarılı giriş: {Email}, Rol: {Role}", user.Email, user.Role);
        return ApiResponse<LoginResponseDto>.SuccessResponse(response, "Giriş başarılı");
    }

    private string GenerateJwtToken(int userId, string email, string role)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"] ?? "60");


// appsettingsdeki secretkeyi byte dizisine çevirip şifreleme için anahtar oluşturma kodu
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // token imzalamak için

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        }; // token içine gömülecek bilgiler

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
