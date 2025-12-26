namespace dotnet_project.DTOs;

// API yanıtları wrapper sınıfı
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    // Başarılı 
    public static ApiResponse<T> SuccessResponse(T data, string message = "İşlem başarılı")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
    // Hata 
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
