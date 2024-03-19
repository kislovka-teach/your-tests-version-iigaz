using AutoMapper;
using Variant1.Dtos;
using Variant1.Models;

namespace Variant1.Profiles.Resolvers;

public class VisitorsValueResolver : IValueResolver<Display, DisplayDto, int>
{
    public int Resolve(Display source, DisplayDto destination, int destMember, ResolutionContext context)
    {
        return source.Visitors.Count;
    }
}