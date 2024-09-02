using System.ComponentModel.DataAnnotations;

namespace WritersPlatform.Models;

public class CompositionModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указано название")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Не указана обложка")]
    public string ImagePath { get; set; } = null!;

    [Required(ErrorMessage = "Не указано содержание")]
    public string Path { get; set; } = null!;

    [Required(ErrorMessage = "Не указано описание")]
    public string Description { get; set; } = null!;

    public float? Rating { get; set; } = 0;
    public DateOnly CreateDate { get; set; }
    public virtual GenreModel Genre { get; set; } = null!;
    public virtual AuthorModel Author { get; set; } = null!;
}
