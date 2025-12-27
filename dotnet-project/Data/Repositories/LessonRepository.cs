using Microsoft.EntityFrameworkCore;
using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId)
    {
        return await _dbSet.Where(l => l.CourseId == courseId).OrderBy(l => l.Order).ToListAsync();
    }
}
