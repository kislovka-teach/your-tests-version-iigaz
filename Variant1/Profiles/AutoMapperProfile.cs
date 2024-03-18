using AutoMapper;
using Variant1.Dtos;
using Variant1.Models;

namespace Variant1.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Plant, PlantDto>();
    }
}