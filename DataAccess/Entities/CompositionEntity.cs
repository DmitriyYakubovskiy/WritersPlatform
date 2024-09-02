namespace WritersPlatform.DataAccess.Entities;

public class CompositionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int GenreId {  get; set; }  
    public int AuthorId { get; set; }
    public DateOnly CreateDate { get; set; }
    public virtual GenreEntity Genre { get; set; } = null!;
    public virtual AuthorEntity Author { get; set; } = null!;
    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}
