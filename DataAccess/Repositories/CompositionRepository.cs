using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WritersPlatform.DataAccess.Contexts;
using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public class CompositionRepository : ICompositionRepository
{
    private readonly CompositionDbContext dbContext;

    public CompositionRepository(CompositionDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(CompositionEntity entity)
    {
        dbContext.Compositions.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Compositions.Remove(entity);
        dbContext.SaveChanges();
    }

    public CompositionEntity[] GetAll()
    {
        return dbContext.Compositions
                        .Include(x => x.Author)
                        .Include(x => x.Genre)
                        .ToArray();
    }

    public CompositionEntity[] GetAllOnlyRead()
    {
        return dbContext.Set<CompositionEntity>()
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Genre)
            .ToArray();
    }

    public CompositionEntity GetById(int id)
    {
        return dbContext.Compositions
                        .Include(x => x.Author)
                        .Include(x => x.Genre)
                        .FirstOrDefault(x => x.Id == id)!;
    }

    public CompositionEntity GetByIdOnlyRead(int id)
    {
        return dbContext.Set<CompositionEntity>()
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Genre)
            .FirstOrDefault(x => x.Id == id)!;
    }

    public float? GetRating(int id)
    {
        return dbContext.Comments.Where(c => c.CompositionId == id).Average(d => (float?)d.Rating);
    }

    public CompositionEntity[] SearchCompositions(int? genreId = null, int? authorId = null, bool? earlier = null, bool? ratingHigher = null)
    {
        var query = dbContext.Compositions.AsQueryable();

        if (genreId.HasValue) query = query.Where(c => c.GenreId == genreId.Value);
        if (authorId.HasValue) query = query.Where(c => c.AuthorId == authorId.Value);
        if (earlier.HasValue)
        {
            if (earlier==true) query = query.OrderByDescending(c => c.CreateDate);
            else query = query.OrderBy(c => c.CreateDate);
        }
        if (ratingHigher.HasValue)
        {
            if (ratingHigher==true) query = query.OrderByDescending(c => c.Comments.Where(c => c.Id == c.Id).Average(d => (double?)d.Rating));
            else query = query.OrderBy(c => c.Comments.Where(c => c.Id == c.Id).Average(d => (double?)d.Rating));
        }

        return query.ToArray();
    }

    public void Update(CompositionEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
