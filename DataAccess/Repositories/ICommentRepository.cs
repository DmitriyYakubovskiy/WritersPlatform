using WritersPlatform.DataAccess.Entities;

namespace WritersPlatform.DataAccess.Repositories;

public interface ICommentRepository
{
    CommentEntity[] GetAll();
    CommentEntity[] GetCommentsFromComposition(int compositionId);
    CommentEntity GetById(int id);
    void Create(CommentEntity entity);
    void Update(CommentEntity entity);
    void Delete(int id);
    void DeleteCommentsFromComposition(int compositionId);
}
