using WritersPlatform.Models;

namespace WritersPlatform.ViewModels;

public class CreateCompositionViewModel
{
    public CompositionModel Composition { get; set; } = null!;
    public GenreModel[] Genres { get; set; } = null!;
    public IFormFile ImageFile { get; set; } = null!;
}
