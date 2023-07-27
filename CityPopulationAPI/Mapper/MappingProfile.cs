using AutoMapper;
using CityPopulationAPI.Models;

namespace CityPopulationAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Country));
        }
    }
}