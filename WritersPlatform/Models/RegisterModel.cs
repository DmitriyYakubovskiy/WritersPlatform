using System.ComponentModel.DataAnnotations;

namespace WritersPlatform.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Не указано имя")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Не указан Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароль введен неверно")]
    public string ConfirmPassword { get; set; } = null!;

    //public string Role { get; set; } = "User";
}
