using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, Product>();
        CreateMap<Category, Category>();
        CreateMap<User, User>();
    }
}
