using AutoMapper;
using Variant2.Dtos;
using Variant2.Models;

namespace Variant2.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Article, ArticlesInListDto>();
        CreateMap<Revision, RevisionDto>();
        CreateMap<Comment, CommentDto>();
    }
}