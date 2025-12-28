using dotnet_project.Data.Repositories;
using dotnet_project.DTOs;
using dotnet_project.DTOs.User;
using dotnet_project.Entities;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<IEnumerable<UserResponseDto>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var result = users.Select(MapToDto);
        return ApiResponse<IEnumerable<UserResponseDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<UserResponseDto>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ApiResponse<UserResponseDto>.ErrorResponse("Kullanıcı bulunamadı");

        return ApiResponse<UserResponseDto>.SuccessResponse(MapToDto(user));
    }

    public async Task<ApiResponse<UserResponseDto>> CreateAsync(CreateUserDto dto)
    {
        // Email kontrolü
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email!);
        if (existingUser != null)
            return ApiResponse<UserResponseDto>.ErrorResponse("Bu email adresi zaten kullanımda");

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = dto.Password 
        };

        await _userRepository.AddAsync(user);
        return ApiResponse<UserResponseDto>.SuccessResponse(MapToDto(user), "Kullanıcı oluşturuldu");
    }

    public async Task<ApiResponse<UserResponseDto>> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ApiResponse<UserResponseDto>.ErrorResponse("Kullanıcı bulunamadı");

        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName ?? user.LastName;
        user.Email = dto.Email ?? user.Email;
        if (!string.IsNullOrEmpty(dto.Password))
            user.PasswordHash = dto.Password;

        await _userRepository.UpdateAsync(user);
        return ApiResponse<UserResponseDto>.SuccessResponse(MapToDto(user), "Kullanıcı güncellendi");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ApiResponse<bool>.ErrorResponse("Kullanıcı bulunamadı");

        await _userRepository.DeleteAsync(user);
        return ApiResponse<bool>.SuccessResponse(true, "Kullanıcı silindi");
    }

    private static UserResponseDto MapToDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}
