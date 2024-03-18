using AutoMapper;
using Variant1.Dtos;
using Variant1.Models;
using Variant1.Profiles.Resolvers;

namespace Variant1.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Plant, PlantDto>();
        CreateMap<Display, DisplayDto>().ForMember(dto => dto.Visitors,
            expression => expression.MapFrom(new VisitorsValueResolver()));
    }
}