using dotnet_project.DTOs;
using dotnet_project.DTOs.Enrollment;

namespace dotnet_project.Services.Interfaces;

public interface IEnrollmentService
{
    Task<ApiResponse<IEnumerable<EnrollmentResponseDto>>> GetAllAsync();
    Task<ApiResponse<EnrollmentResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<EnrollmentResponseDto>> CreateAsync(CreateEnrollmentDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
