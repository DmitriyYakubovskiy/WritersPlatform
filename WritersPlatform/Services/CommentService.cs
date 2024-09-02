using AutoMapper;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.DataAccess.Repositories;
using WritersPlatform.Models;

namespace WritersPlatform.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IMapper mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        this.commentRepository = commentRepository;
        this.mapper = mapper;
    }

    public void Create(CommentModel model)
    {
        commentRepository.Create(mapper.Map<CommentEntity>(model));
    }

    public void Delete(int id)
    {
        commentRepository.Delete(id);
    }

    public void DeleteCommentsFromComposition(int compositionId)
    {
        commentRepository.DeleteCommentsFromComposition(compositionId);
    }

    public CommentModel[] GetAll()
    {
        return mapper.Map<CommentModel[]>(commentRepository.GetAll());
    }

    public CommentModel GetById(int id)
    {
        var entity = commentRepository.GetById(id);
        return mapper.Map<CommentModel>(entity);
    }

    public CommentModel[] GetCommentsFromComposition(int compositionId)
    {
        return mapper.Map<CommentModel[]>(commentRepository.GetCommentsFromComposition(compositionId));
    }

    public void Update(CommentModel model)
    {
        var oldEntity = commentRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        oldEntity.Rating = model.Rating;
        oldEntity.Text = model.Text;
        oldEntity.AuthorId=model.Author.Id;
        oldEntity.CompositionId = model.Composition.Id;

        commentRepository.Update(oldEntity);
    }
}
