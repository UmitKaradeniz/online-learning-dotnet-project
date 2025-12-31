using dotnet_project.DTOs;
using dotnet_project.DTOs.Auth;

namespace dotnet_project.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto);
}
