using WritersPlatform.DataAccess.Contexts;
using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly CompositionDbContext dbContext;

    public AuthorRepository(CompositionDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(AuthorEntity entity)
    {
        dbContext.Authors.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Authors.Remove(entity);
        dbContext.SaveChanges();
    }

    public AuthorEntity[] GetAll()
    {
        return dbContext.Authors.ToArray();
    }

    public AuthorEntity GetByEmail(string email)
    {
        return dbContext.Authors.FirstOrDefault(x => x.Email == email)!;
    }

    public AuthorEntity GetById(int id)
    {
        return dbContext.Authors.FirstOrDefault(x => x.Id == id)!;
    }

    public AuthorEntity GetByUserId(string id)
    {
        return dbContext.Authors.FirstOrDefault(x => x.UserId == id)!;
    }

    public void Update(AuthorEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
