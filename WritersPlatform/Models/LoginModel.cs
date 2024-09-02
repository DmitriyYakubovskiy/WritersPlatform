using System.ComponentModel.DataAnnotations;

namespace WritersPlatform.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Не указана почта")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string Role { get; set; } = "User";
    public string ReturnUrl { get; set; } = "/";
    public bool Remember { get; set; }
}
