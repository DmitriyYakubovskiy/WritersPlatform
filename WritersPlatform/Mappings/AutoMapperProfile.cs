using AutoMapper;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.Models;

namespace Library.Mappings;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CompositionModel, CompositionEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest=>dest.CreateDate, opt=>opt.MapFrom(src=>src.CreateDate))
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

        CreateMap<AuthorModel, AuthorEntity>().ReverseMap();
        CreateMap<GenreModel, GenreEntity>().ReverseMap();
        CreateMap<CommentModel, CommentEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src=>src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.Composition, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Composition, opt => opt.MapFrom(src => src.Composition));
    }
}
