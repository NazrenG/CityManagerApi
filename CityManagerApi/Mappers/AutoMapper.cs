using AutoMapper;
using CityManagerApi.Dtos;
using CityManagerApi.Entities;

namespace CityManagerApi.Mappers
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<City,CityDto>().ReverseMap();

            CreateMap<City, CityForListDto>().ForMember(p => p.PhotUrl, opt =>
            {
                opt.MapFrom(i => i.CityImages.FirstOrDefault(m => m.IsMain).Url);
            });
        }
    }
}
