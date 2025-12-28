using dotnet_project.Data.Repositories;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Enrollment;
using dotnet_project.Entities;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IUserRepository userRepository,
        ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ApiResponse<IEnumerable<EnrollmentResponseDto>>> GetAllAsync()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        var result = new List<EnrollmentResponseDto>();
        
        foreach (var enrollment in enrollments)
        {
            var user = await _userRepository.GetByIdAsync(enrollment.UserId);
            var course = await _courseRepository.GetByIdAsync(enrollment.CourseId);
            result.Add(MapToDto(enrollment, user, course));
        }
        
        return ApiResponse<IEnumerable<EnrollmentResponseDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<EnrollmentResponseDto>> GetByIdAsync(int id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment == null)
            return ApiResponse<EnrollmentResponseDto>.ErrorResponse("Kayıt bulunamadı");

        var user = await _userRepository.GetByIdAsync(enrollment.UserId);
        var course = await _courseRepository.GetByIdAsync(enrollment.CourseId);
        return ApiResponse<EnrollmentResponseDto>.SuccessResponse(MapToDto(enrollment, user, course));
    }

    public async Task<ApiResponse<EnrollmentResponseDto>> CreateAsync(CreateEnrollmentDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            return ApiResponse<EnrollmentResponseDto>.ErrorResponse("Kullanıcı bulunamadı");

        var course = await _courseRepository.GetByIdAsync(dto.CourseId);
        if (course == null)
            return ApiResponse<EnrollmentResponseDto>.ErrorResponse("Kurs bulunamadı");

        // Aynı kursa tekrar kayıt 
        var existing = await _enrollmentRepository.GetByUserAndCourseAsync(dto.UserId, dto.CourseId);
        if (existing != null)
            return ApiResponse<EnrollmentResponseDto>.ErrorResponse("Kullanıcı zaten bu kursa kayıtlı");

        var enrollment = new Enrollment
        {
            UserId = dto.UserId,
            CourseId = dto.CourseId,
            EnrolledAt = DateTime.UtcNow
        };

        await _enrollmentRepository.AddAsync(enrollment);
        return ApiResponse<EnrollmentResponseDto>.SuccessResponse(MapToDto(enrollment, user, course), "Kayıt oluşturuldu");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment == null)
            return ApiResponse<bool>.ErrorResponse("Kayıt bulunamadı");

        await _enrollmentRepository.DeleteAsync(enrollment);
        return ApiResponse<bool>.SuccessResponse(true, "Kayıt silindi");
    }

    private static EnrollmentResponseDto MapToDto(Enrollment enrollment, User? user, Course? course)
    {
        return new EnrollmentResponseDto
        {
            Id = enrollment.Id,
            UserId = enrollment.UserId,
            UserName = user != null ? $"{user.FirstName} {user.LastName}" : null,
            CourseId = enrollment.CourseId,
            CourseName = course?.Title,
            EnrolledAt = enrollment.EnrolledAt,
            CreatedAt = enrollment.CreatedAt,
            UpdatedAt = enrollment.UpdatedAt
        };
    }
}
