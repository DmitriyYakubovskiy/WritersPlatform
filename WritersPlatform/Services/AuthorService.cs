using AutoMapper;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.DataAccess.Repositories;
using WritersPlatform.Models;

namespace WritersPlatform.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository authorRepository;
    private readonly IMapper mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        this.authorRepository = authorRepository;
        this.mapper = mapper;
    }

    public void Create(AuthorModel model)
    {
        authorRepository.Create(mapper.Map<AuthorEntity>(model));
    }

    public void Delete(int id)
    {
        authorRepository.Delete(id);
    }

    public AuthorModel[] GetAll()
    {
        return mapper.Map<AuthorModel[]>(authorRepository.GetAll());
    }

    public AuthorModel GetByEmail(string email)
    {
        var entity = authorRepository.GetByEmail(email);
        return mapper.Map<AuthorModel>(entity);
    }

    public AuthorModel GetById(int id)
    {
        var entity = authorRepository.GetById(id);
        return mapper.Map<AuthorModel>(entity);
    }

    public AuthorModel GetByUserId(string userId)
    {
        var entity = authorRepository.GetByUserId(userId);
        return mapper.Map<AuthorModel>(entity);
    }

    public void Update(AuthorModel model)
    {
        var oldEntity = authorRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        oldEntity.Email = model.Email;

        authorRepository.Update(oldEntity);
    }
}
