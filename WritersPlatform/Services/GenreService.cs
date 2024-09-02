using AutoMapper;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.DataAccess.Repositories;
using WritersPlatform.Models;

namespace WritersPlatform.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository genreRepository;
    private readonly IMapper mapper;

    public GenreService(IGenreRepository genreRepository, IMapper mapper)
    {
        this.genreRepository = genreRepository;
        this.mapper = mapper;
    }

    public void Create(GenreModel model)
    {
        genreRepository.Create(mapper.Map<GenreEntity>(model));
    }

    public void Delete(int id)
    {
        genreRepository.Delete(id);
    }

    public GenreModel[] GetAll()
    {
        return mapper.Map<GenreModel[]>(genreRepository.GetAll());
    }

    public GenreModel GetById(int id)
    {
        var entity = genreRepository.GetById(id);
        return mapper.Map<GenreModel>(entity);
    }

    public void Update(GenreModel model)
    {
        var oldEntity = genreRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Name = model.Name;

        genreRepository.Update(oldEntity);
    }
}
