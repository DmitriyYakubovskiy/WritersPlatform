namespace WritersPlatform.Models;

public class SortCompositionModel
{
    public int? AuthorId { get; set; }
    public int? GenreId { get; set; }
    public bool? Earlier { get; set; }
    public bool? RatingHigher { get; set; }
}
