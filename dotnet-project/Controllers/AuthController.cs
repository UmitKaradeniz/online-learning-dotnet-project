using Microsoft.AspNetCore.Mvc;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Auth;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    { _authService = authService; }
    
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.Success)
            return Unauthorized(result);
        return Ok(result);
    }
}
