using dotnet_project.Data.Repositories;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Lesson;
using dotnet_project.Entities;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ICourseRepository _courseRepository;

    public LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository)
    {
        _lessonRepository = lessonRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ApiResponse<IEnumerable<LessonResponseDto>>> GetAllAsync()
    {
        var lessons = await _lessonRepository.GetAllAsync();
        var result = new List<LessonResponseDto>();
        
        foreach (var lesson in lessons)
        {
            var course = await _courseRepository.GetByIdAsync(lesson.CourseId);
            result.Add(MapToDto(lesson, course));
        }
        
        return ApiResponse<IEnumerable<LessonResponseDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<IEnumerable<LessonResponseDto>>> GetByCourseIdAsync(int courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
            return ApiResponse<IEnumerable<LessonResponseDto>>.ErrorResponse("Kurs bulunamadı");

        var lessons = await _lessonRepository.GetByCourseIdAsync(courseId);
        var result = lessons.Select(l => MapToDto(l, course));
        return ApiResponse<IEnumerable<LessonResponseDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<LessonResponseDto>> GetByIdAsync(int id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null)
            return ApiResponse<LessonResponseDto>.ErrorResponse("Ders bulunamadı");

        var course = await _courseRepository.GetByIdAsync(lesson.CourseId);
        return ApiResponse<LessonResponseDto>.SuccessResponse(MapToDto(lesson, course));
    }

    public async Task<ApiResponse<LessonResponseDto>> CreateAsync(CreateLessonDto dto)
    {
        var course = await _courseRepository.GetByIdAsync(dto.CourseId);
        if (course == null)
            return ApiResponse<LessonResponseDto>.ErrorResponse("Kurs bulunamadı");

        var lesson = new Lesson
        {
            Title = dto.Title,
            Content = dto.Content,
            Duration = dto.Duration,
            Order = dto.Order,
            CourseId = dto.CourseId
        };

        await _lessonRepository.AddAsync(lesson);
        return ApiResponse<LessonResponseDto>.SuccessResponse(MapToDto(lesson, course), "Ders oluşturuldu");
    }

    public async Task<ApiResponse<LessonResponseDto>> UpdateAsync(int id, UpdateLessonDto dto)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null)
            return ApiResponse<LessonResponseDto>.ErrorResponse("Ders bulunamadı");

        lesson.Title = dto.Title ?? lesson.Title;
        lesson.Content = dto.Content ?? lesson.Content;
        lesson.Duration = dto.Duration;
        lesson.Order = dto.Order;

        await _lessonRepository.UpdateAsync(lesson);
        var course = await _courseRepository.GetByIdAsync(lesson.CourseId);
        return ApiResponse<LessonResponseDto>.SuccessResponse(MapToDto(lesson, course), "Ders güncellendi");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null)
            return ApiResponse<bool>.ErrorResponse("Ders bulunamadı");

        await _lessonRepository.DeleteAsync(lesson);
        return ApiResponse<bool>.SuccessResponse(true, "Ders silindi");
    }

    private static LessonResponseDto MapToDto(Lesson lesson, Course? course)
    {
        return new LessonResponseDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Duration = lesson.Duration,
            Order = lesson.Order,
            CourseId = lesson.CourseId,
            CourseName = course?.Title,
            CreatedAt = lesson.CreatedAt,
            UpdatedAt = lesson.UpdatedAt
        };
    }
}
