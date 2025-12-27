using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<Course>> GetByInstructorIdAsync(int instructorId);
    Task<Course?> GetWithLessonsAsync(int id);
}
