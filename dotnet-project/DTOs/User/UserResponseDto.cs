namespace dotnet_project.DTOs.User;

// Kullanıcı yanıt DTO'su (şifre gibi hassas bilgiler dahil değil)
public class UserResponseDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
