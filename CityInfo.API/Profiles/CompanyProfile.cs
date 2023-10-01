using AutoMapper;


namespace CompanyInfo.API.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Entities.Company, Models.CompanyWithoutCarModelDto>();
            CreateMap<Entities.Company, Models.CompanyDto>();
        }
    }
}
