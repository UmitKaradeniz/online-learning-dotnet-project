using dotnet_project.DTOs;
using dotnet_project.DTOs.User;
using dotnet_project.DTOs.Course;
using dotnet_project.DTOs.Lesson;
using dotnet_project.DTOs.Enrollment;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Extensions;

public static class MinimalApiEndpoints
{
    public static void MapMinimalApiEndpoints(this WebApplication app)
    {
        // minimal api v2 kısmı - users
        var users = app.MapGroup("/api/v2/users").WithTags("Users (Minimal API)");
        
        users.MapGet("/", async (IUserService service) =>
        {
            var result = await service.GetAllAsync();
            return Results.Ok(result);
        });

        users.MapGet("/{id}", async (int id, IUserService service) =>
        {
            var result = await service.GetByIdAsync(id);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        users.MapPost("/", async (CreateUserDto dto, IUserService service) =>
        {
            var result = await service.CreateAsync(dto);
            return result.Success 
                ? Results.Created($"/api/v2/users/{result.Data?.Id}", result) 
                : Results.Conflict(result);
        });

        users.MapPut("/{id}", async (int id, UpdateUserDto dto, IUserService service) =>
        {
            var result = await service.UpdateAsync(id, dto);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        users.MapDelete("/{id}", async (int id, IUserService service) =>
        {
            var result = await service.DeleteAsync(id);
            return result.Success ? Results.NoContent() : Results.NotFound(result);
        });

        // minimal api v2 kısmı - courses 
        var courses = app.MapGroup("/api/v2/courses").WithTags("Courses (Minimal API)");
        
        courses.MapGet("/", async (ICourseService service) =>
        {
            var result = await service.GetAllAsync();
            return Results.Ok(result);
        });

        courses.MapGet("/{id}", async (int id, ICourseService service) =>
        {
            var result = await service.GetByIdAsync(id);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        courses.MapPost("/", async (CreateCourseDto dto, ICourseService service) =>
        {
            var result = await service.CreateAsync(dto);
            return result.Success 
                ? Results.Created($"/api/v2/courses/{result.Data?.Id}", result) 
                : Results.BadRequest(result);
        });

        courses.MapPut("/{id}", async (int id, UpdateCourseDto dto, ICourseService service) =>
        {
            var result = await service.UpdateAsync(id, dto);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        courses.MapDelete("/{id}", async (int id, ICourseService service) =>
        {
            var result = await service.DeleteAsync(id);
            return result.Success ? Results.NoContent() : Results.NotFound(result);
        });

        // minimal api v2 kısmı - lessons 
        var lessons = app.MapGroup("/api/v2/lessons").WithTags("Lessons (Minimal API)");
        
        lessons.MapGet("/", async (ILessonService service) =>
        {
            var result = await service.GetAllAsync();
            return Results.Ok(result);
        });

        lessons.MapGet("/{id}", async (int id, ILessonService service) =>
        {
            var result = await service.GetByIdAsync(id);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        lessons.MapGet("/course/{courseId}", async (int courseId, ILessonService service) =>
        {
            var result = await service.GetByCourseIdAsync(courseId);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        lessons.MapPost("/", async (CreateLessonDto dto, ILessonService service) =>
        {
            var result = await service.CreateAsync(dto);
            return result.Success 
                ? Results.Created($"/api/v2/lessons/{result.Data?.Id}", result) 
                : Results.BadRequest(result);
        });

        lessons.MapPut("/{id}", async (int id, UpdateLessonDto dto, ILessonService service) =>
        {
            var result = await service.UpdateAsync(id, dto);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        lessons.MapDelete("/{id}", async (int id, ILessonService service) =>
        {
            var result = await service.DeleteAsync(id);
            return result.Success ? Results.NoContent() : Results.NotFound(result);
        });

        // minimal api v2 kısmı - enrollments 
        var enrollments = app.MapGroup("/api/v2/enrollments").WithTags("Enrollments (Minimal API)");
        
        enrollments.MapGet("/", async (IEnrollmentService service) =>
        {
            var result = await service.GetAllAsync();
            return Results.Ok(result);
        });

        enrollments.MapGet("/{id}", async (int id, IEnrollmentService service) =>
        {
            var result = await service.GetByIdAsync(id);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        });

        enrollments.MapPost("/", async (CreateEnrollmentDto dto, IEnrollmentService service) =>
        {
            var result = await service.CreateAsync(dto);
            if (!result.Success)
                return result.Message?.Contains("zaten") == true 
                    ? Results.Conflict(result) 
                    : Results.BadRequest(result);
            return Results.Created($"/api/v2/enrollments/{result.Data?.Id}", result);
        });

        enrollments.MapDelete("/{id}", async (int id, IEnrollmentService service) =>
        {
            var result = await service.DeleteAsync(id);
            return result.Success ? Results.NoContent() : Results.NotFound(result);
        });
    }
}
