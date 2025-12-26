namespace dotnet_project.DTOs.User;

// Yeni kullanıcı oluşturma DTO'su
public class CreateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
