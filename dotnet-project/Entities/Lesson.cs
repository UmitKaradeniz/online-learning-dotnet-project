namespace dotnet_project.Entities;

public class Lesson : BaseEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Duration { get; set; } 
    public int Order { get; set; }
    public int CourseId { get; set; }

    public Course? Course { get; set; }
}
