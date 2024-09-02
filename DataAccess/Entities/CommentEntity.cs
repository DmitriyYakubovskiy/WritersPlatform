namespace WritersPlatform.DataAccess.Entities;

public class CommentEntity
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int Rating { get; set; } = 0;
    public int AuthorId { get; set; }
    public int CompositionId {  get; set; }
    public virtual AuthorEntity Author { get; set; } = null!;
    public virtual CompositionEntity Composition { get; set; } = null!;
}
