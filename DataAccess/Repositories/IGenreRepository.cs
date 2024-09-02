using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public interface IGenreRepository
{
    GenreEntity[] GetAll();
    GenreEntity GetById(int id);
    void Create(GenreEntity entity);
    void Update(GenreEntity entity);
    void Delete(int id);
}
