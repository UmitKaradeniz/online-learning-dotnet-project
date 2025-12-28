using dotnet_project.Data.Repositories;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Course;
using dotnet_project.Entities;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<IEnumerable<CourseResponseDto>>> GetAllAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        var result = new List<CourseResponseDto>();
        
        foreach (var course in courses)
        {
            var instructor = await _userRepository.GetByIdAsync(course.InstructorId);
            result.Add(MapToDto(course, instructor));
        }
        
        return ApiResponse<IEnumerable<CourseResponseDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<CourseResponseDto>> GetByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            return ApiResponse<CourseResponseDto>.ErrorResponse("Kurs bulunamadı");

        var instructor = await _userRepository.GetByIdAsync(course.InstructorId);
        return ApiResponse<CourseResponseDto>.SuccessResponse(MapToDto(course, instructor));
    }

    public async Task<ApiResponse<CourseResponseDto>> CreateAsync(CreateCourseDto dto)
    {
        var instructor = await _userRepository.GetByIdAsync(dto.InstructorId);
        if (instructor == null)
            return ApiResponse<CourseResponseDto>.ErrorResponse("Eğitmen bulunamadı");

        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            InstructorId = dto.InstructorId
        };

        await _courseRepository.AddAsync(course);
        return ApiResponse<CourseResponseDto>.SuccessResponse(MapToDto(course, instructor), "Kurs oluşturuldu");
    }

    public async Task<ApiResponse<CourseResponseDto>> UpdateAsync(int id, UpdateCourseDto dto)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            return ApiResponse<CourseResponseDto>.ErrorResponse("Kurs bulunamadı");

        course.Title = dto.Title ?? course.Title;
        course.Description = dto.Description ?? course.Description;
        course.Price = dto.Price;
        course.InstructorId = dto.InstructorId;

        await _courseRepository.UpdateAsync(course);
        var instructor = await _userRepository.GetByIdAsync(course.InstructorId);
        return ApiResponse<CourseResponseDto>.SuccessResponse(MapToDto(course, instructor), "Kurs güncellendi");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            return ApiResponse<bool>.ErrorResponse("Kurs bulunamadı");

        await _courseRepository.DeleteAsync(course);
        return ApiResponse<bool>.SuccessResponse(true, "Kurs silindi");
    }

    private static CourseResponseDto MapToDto(Course course, User? instructor)
    {
        return new CourseResponseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            InstructorId = course.InstructorId,
            InstructorName = instructor != null ? $"{instructor.FirstName} {instructor.LastName}" : null,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt
        };
    }
}
