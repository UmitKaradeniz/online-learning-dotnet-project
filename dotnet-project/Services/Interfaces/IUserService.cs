using dotnet_project.DTOs;
using dotnet_project.DTOs.User;

namespace dotnet_project.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<IEnumerable<UserResponseDto>>> GetAllAsync();
    Task<ApiResponse<UserResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<UserResponseDto>> CreateAsync(CreateUserDto dto);
    Task<ApiResponse<UserResponseDto>> UpdateAsync(int id, UpdateUserDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
