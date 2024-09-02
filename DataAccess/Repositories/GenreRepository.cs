using Microsoft.EntityFrameworkCore;
using WritersPlatform.DataAccess.Contexts;
using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly CompositionDbContext dbContext;

    public GenreRepository(CompositionDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(GenreEntity entity)
    {
        dbContext.Genres.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Genres.Remove(entity);
        dbContext.SaveChanges();
    }

    public GenreEntity[] GetAll()
    {
        return dbContext.Genres.ToArray();
    }

    public GenreEntity GetById(int id)
    {
        return dbContext.Genres.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(GenreEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
