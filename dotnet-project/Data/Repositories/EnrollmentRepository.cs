using Microsoft.EntityFrameworkCore;
using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Enrollment>> GetByUserIdAsync(int userId)
    {
        return await _dbSet.Where(e => e.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId)
    {
        return await _dbSet.Where(e => e.CourseId == courseId).ToListAsync();
    }

    // belirli bir kursa kayıtlı olup olmadığı
    public async Task<Enrollment?> GetByUserAndCourseAsync(int userId, int courseId)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
    }
}
