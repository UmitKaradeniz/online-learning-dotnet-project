namespace dotnet_project.Entities;

public class Enrollment : BaseEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public Course? Course { get; set; }
}
