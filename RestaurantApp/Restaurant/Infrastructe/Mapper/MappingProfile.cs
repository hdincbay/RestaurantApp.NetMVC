using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Restaurant.Infrastructe.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertion, Product>();
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap();
        }
    }
}