using CompanyInfo.API.Entities;

namespace CompanyInfo.API.Repositories
{
    public interface ICompanyInfoRepository
    {
        
        Task<IEnumerable<Company>> GetCompaniesAsync();  
        Task<Company?> GetCompanyAsync(int companyID , bool includeCarModels);
        Task<IEnumerable<CarModel>> GetCarModelsForCompanyAsync(int companyID);

        Task<CarModel?> GetSingleCarModelForCompanyAsync(int companyID, int carModelID);

        Task<bool> DoesCompanyExist(int companyID);

        //Task<bool> DoesCarModelExist(int carModelID);

        //Task<bool> DoesCompanyAndCarModelExist(int companyID, int carModelID);

        Task AddCarModelForCompanyAsync(int companyID, CarModel carModel);

        Task<bool> SaveChangesAsync();


        
    }
}
