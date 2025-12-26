namespace dotnet_project.DTOs.Course;

public class CreateCourseDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int InstructorId { get; set; }
}
