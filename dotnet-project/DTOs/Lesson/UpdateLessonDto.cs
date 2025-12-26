namespace dotnet_project.DTOs.Lesson;

public class UpdateLessonDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Duration { get; set; }
    public int Order { get; set; }
}
