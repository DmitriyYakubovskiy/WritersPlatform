using WritersPlatform.Models;

namespace WritersPlatform.ViewModels;

public class CompositionViewModel
{
    public CompositionModel Composition { get; set; } = null!;
    public CommentModel[] Comments { get; set; } = null!;
    public int AuthorId { get; set; }
}
