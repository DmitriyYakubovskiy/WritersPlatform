using WritersPlatform.Models;

namespace WritersPlatform.Services;

public interface IGenreService
{
    GenreModel[] GetAll();
    GenreModel GetById(int id);
    void Create(GenreModel model);
    void Update(GenreModel model);
    void Delete(int id);
}
