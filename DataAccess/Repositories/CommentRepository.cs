using WritersPlatform.DataAccess.Contexts;
using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly CompositionDbContext dbContext;

    public CommentRepository(CompositionDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(CommentEntity entity)
    {
        dbContext.Comments.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Comments.Remove(entity);
        dbContext.SaveChanges();
    }

    public void DeleteCommentsFromComposition(int compositionId)
    {
        dbContext.Comments.RemoveRange(dbContext.Comments.Where(x => x.CompositionId == compositionId));
        dbContext.SaveChanges();
    }

    public CommentEntity[] GetAll()
    {
        return dbContext.Comments.ToArray();
    }

    public CommentEntity GetById(int id)
    {
        return dbContext.Comments.FirstOrDefault(x => x.Id == id)!;
    }

    public CommentEntity[] GetCommentsFromComposition(int compositionId)
    {
        return dbContext.Comments.Where(x => x.Composition.Id == compositionId).ToArray();
    }

    public void Update(CommentEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
