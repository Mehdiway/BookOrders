using AutoMapper;
using Catalog.Domain.Entities;
using Shared.DTO;

namespace Catalog.Infrastructure.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, Book>();
    }
}
