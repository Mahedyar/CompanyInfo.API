using AutoMapper;

namespace CompanyInfo.API.Profiles
{
    public class CarModelProfile:Profile
    {
        public CarModelProfile()
        {
            CreateMap<Entities.CarModel, Models.CarModelDto>();
            CreateMap<Entities.CarModel, Models.CarModelUpdateDto>();
            CreateMap<Models.CarModelCreationDto, Entities.CarModel>();
            CreateMap<Models.CarModelUpdateDto, Entities.CarModel>();
            


        }
    }
}
