using dotnet_project.Entities;

namespace dotnet_project.Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
