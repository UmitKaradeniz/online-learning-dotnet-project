using dotnet_project.DTOs;
using dotnet_project.DTOs.Course;

namespace dotnet_project.Services.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<IEnumerable<CourseResponseDto>>> GetAllAsync();
    Task<ApiResponse<CourseResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<CourseResponseDto>> CreateAsync(CreateCourseDto dto);
    Task<ApiResponse<CourseResponseDto>> UpdateAsync(int id, UpdateCourseDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
