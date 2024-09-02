using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public interface ICompositionRepository
{
    CompositionEntity[] GetAll();
    CompositionEntity[] GetAllOnlyRead();
    CompositionEntity GetById(int id);
    CompositionEntity GetByIdOnlyRead(int id);
    CompositionEntity[] SearchCompositions(
        int? genreId = null,
        int? authorId = null,
        bool? earlier = null,
        bool? ratingHigher = null);
    void Create(CompositionEntity entity);
    void Update(CompositionEntity entity);
    void Delete(int id);
    float? GetRating(int id);
}