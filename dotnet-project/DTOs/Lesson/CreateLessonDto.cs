namespace dotnet_project.DTOs.Lesson;

public class CreateLessonDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Duration { get; set; } // dakika 
    public int Order { get; set; }
    public int CourseId { get; set; }
}
