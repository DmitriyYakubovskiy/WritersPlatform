using AutoMapper;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.DataAccess.Repositories;
using WritersPlatform.Models;

namespace WritersPlatform.Services;

public class CompositionService : ICompositionService
{
    private readonly ICompositionRepository compositionRepository;
    private readonly IMapper mapper;

    public CompositionService(ICompositionRepository compositionRepository, IMapper mapper)
    {
        this.compositionRepository = compositionRepository;
        this.mapper = mapper;
    }

    public void Create(CompositionModel model)
    {
        compositionRepository.Create(mapper.Map<CompositionEntity>(model));
    }

    public void Delete(int id)
    {
        compositionRepository.Delete(id);
    }

    public CompositionModel[] GetAll()
    {
        var models = mapper.Map<CompositionModel[]>(compositionRepository.GetAll());
        foreach (var model in models)
            model.Rating = GetRating(model.Id);
        return models;
    }

    public CompositionModel[] GetAllOnlyRead()
    {
        var models =mapper.Map<CompositionModel[]>(compositionRepository.GetAllOnlyRead());

        foreach (var model in models)
            model.Rating = GetRating(model.Id);

        return models;
    }

    public CompositionModel GetById(int id)
    {
        var entity = compositionRepository.GetById(id);
        var model = mapper.Map<CompositionModel>(entity);
        model.Rating = GetRating(id);
        return model;
    }

    public CompositionModel GetByIdOnlyRead(int id)
    {
        var entity = compositionRepository.GetByIdOnlyRead(id);
        var model = mapper.Map<CompositionModel>(entity);
        model.Rating = GetRating(id);
        return model;
    }

    public float? GetRating(int id)
    {
        return compositionRepository.GetRating(id);
    }

    public CompositionModel[] SearchCompositions(int? genreId = null, int? authorId = null, bool? earlier = null, bool? ratingHigher = null)
    {
        var models = mapper.Map<CompositionModel[]>(compositionRepository.SearchCompositions(genreId, authorId, earlier, ratingHigher));
        
        foreach (var model in models)
            model.Rating = GetRating(model.Id);
        
        return models;
    }

    public void Update(CompositionModel model)
    {
        var oldEntity = compositionRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Name = model.Name;
        oldEntity.ImagePath = model.ImagePath;
        oldEntity.AuthorId = model.Author.Id;
        oldEntity.GenreId = model.Genre.Id;
        oldEntity.Path = model.Path;
        oldEntity.Description = model.Description;
        oldEntity.CreateDate = model.CreateDate;

        compositionRepository.Update(oldEntity);
    }
}
