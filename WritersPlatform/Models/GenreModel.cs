using System.ComponentModel.DataAnnotations;

namespace WritersPlatform.Models;

public class GenreModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указано название жанра")]
    public string Name { get; set; } = null!;
}
