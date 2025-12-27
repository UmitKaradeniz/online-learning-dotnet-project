using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public interface ILessonRepository : IRepository<Lesson>
{
    Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId);
}
