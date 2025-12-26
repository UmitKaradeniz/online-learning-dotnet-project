namespace dotnet_project.Entities;

public class Course : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int InstructorId { get; set; }

    public User? Instructor { get; set; }
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
