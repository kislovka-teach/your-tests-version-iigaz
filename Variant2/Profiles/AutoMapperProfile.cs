using AutoMapper;
using Variant1.Dtos;
using Variant2.Models;

namespace Variant2.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Article, ArticlesInListDto>();
    }
}