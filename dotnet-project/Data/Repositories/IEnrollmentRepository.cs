using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public interface IEnrollmentRepository : IRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);
    Task<Enrollment?> GetByUserAndCourseAsync(int userId, int courseId);
}
