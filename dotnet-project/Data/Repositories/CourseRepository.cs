using Microsoft.EntityFrameworkCore;
using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Course>> GetByInstructorIdAsync(int instructorId)
    {
        return await _dbSet.Where(c => c.InstructorId == instructorId).ToListAsync();
    }

    // Kurs ve dersleri birlikte getir
    public async Task<Course?> GetWithLessonsAsync(int id)
    {
        return await _dbSet.Include(c => c.Lessons).FirstOrDefaultAsync(c => c.Id == id);
    }
}
