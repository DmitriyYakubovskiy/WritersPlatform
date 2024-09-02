namespace WritersPlatform.DataAccess.Entities;

public class GenreEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<CompositionEntity> Compositions { get; set; } = new List<CompositionEntity>();
}
