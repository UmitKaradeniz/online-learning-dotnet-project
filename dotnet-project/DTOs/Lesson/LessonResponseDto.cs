namespace dotnet_project.DTOs.Lesson;

public class LessonResponseDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
