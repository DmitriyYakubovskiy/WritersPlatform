using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public interface IAuthorRepository
{
    AuthorEntity[] GetAll();
    AuthorEntity GetById(int id);
    AuthorEntity GetByUserId(string id);
    AuthorEntity GetByEmail(string email);
    void Create(AuthorEntity entity);
    void Update(AuthorEntity entity);
    void Delete(int id);
}
