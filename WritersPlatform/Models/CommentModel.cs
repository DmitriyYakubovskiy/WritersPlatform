namespace WritersPlatform.Models;

public class CommentModel
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int Rating { get; set; }= 0;
    public AuthorModel Author { get; set; } = null!;
    public CompositionModel Composition { get; set; } = null!;
}
