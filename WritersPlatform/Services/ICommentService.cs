using WritersPlatform.Models;

namespace WritersPlatform.Services;

public interface ICommentService
{
    CommentModel[] GetAll();
    CommentModel[] GetCommentsFromComposition(int compositionId);
    CommentModel GetById(int id);
    void Create(CommentModel model);
    void Update(CommentModel model);
    void DeleteCommentsFromComposition(int compositionId);
    void Delete(int id);
}
