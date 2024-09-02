using WritersPlatform.DataAccess.Entities;
using WritersPlatform.Models;

namespace WritersPlatform.Services;

public interface IAuthorService
{
    AuthorModel[] GetAll();
    AuthorModel GetById(int id);
    AuthorModel GetByUserId(string userId);
    AuthorModel GetByEmail(string email);
    void Create(AuthorModel model);
    void Update(AuthorModel model);
    void Delete(int id);
}
