using dotnet_project.DTOs;
using dotnet_project.DTOs.Lesson;

namespace dotnet_project.Services.Interfaces;

public interface ILessonService
{
    Task<ApiResponse<IEnumerable<LessonResponseDto>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<LessonResponseDto>>> GetByCourseIdAsync(int courseId);
    Task<ApiResponse<LessonResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<LessonResponseDto>> CreateAsync(CreateLessonDto dto);
    Task<ApiResponse<LessonResponseDto>> UpdateAsync(int id, UpdateLessonDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
