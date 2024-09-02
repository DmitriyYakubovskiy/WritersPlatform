using WritersPlatform.DataAccess.Entities;
using WritersPlatform.Models;

namespace WritersPlatform.Services;

public interface ICompositionService
{
    CompositionModel[] GetAll();
    CompositionModel[] GetAllOnlyRead();
    CompositionModel GetById(int id);
    CompositionModel GetByIdOnlyRead(int id);
    CompositionModel[] SearchCompositions(int? genreId = null, int? authorId = null, bool? earlier = null, bool? ratingHigher = null);
    void Create(CompositionModel model);
    void Update(CompositionModel model);
    void Delete(int id);
    float? GetRating(int id);
}
