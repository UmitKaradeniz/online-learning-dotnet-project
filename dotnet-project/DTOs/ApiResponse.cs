namespace dotnet_project.DTOs;

// Tüm API yanıtları için standart wrapper sınıfı
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    // Başarılı yanıt oluşturur
    public static ApiResponse<T> SuccessResponse(T data, string message = "İşlem başarılı")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    // Hata yanıtı oluşturur
    public static ApiResponse<T> ErrorResponse(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
    }
}
