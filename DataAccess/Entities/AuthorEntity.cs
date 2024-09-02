namespace WritersPlatform.DataAccess.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<CompositionEntity> Compositions { get; set; } = new List<CompositionEntity>();
    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}
