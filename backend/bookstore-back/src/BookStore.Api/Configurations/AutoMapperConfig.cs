using AutoMapper;
using BookStore.Api.ViewModels;
using BookStore.Business.Models;

namespace BookStore.Api.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig() 
    {
        CreateMap<Autor, AutorViewModel>().ReverseMap();
        CreateMap<Assunto, AssuntoViewModel>().ReverseMap();
        CreateMap<Livro, LivroViewModel>().ReverseMap();
    }
}
