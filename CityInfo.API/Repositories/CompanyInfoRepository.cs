using CompanyInfo.API.DbContexts;
using CompanyInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfo.API.Repositories
{
    public class CompanyInfoRepository : ICompanyInfoRepository
    {
        private readonly CompanyInfoDbContext _context;

        public CompanyInfoRepository(CompanyInfoDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<CarModel?> GetSingleCarModelForCompanyAsync(int companyID, int carModelID)
        {
            return await _context.CarModels.Where(carModel => carModel.CompanyID == companyID && carModel.ID == carModelID).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CarModel>> GetCarModelsForCompanyAsync(int companyID)
        {
            return await _context.CarModels.Where(CarModel => CarModel.CompanyID == companyID).ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.OrderBy(company => company.Name).ToListAsync();
        }

        public async Task<Company?> GetCompanyAsync(int companyID , bool includeCarModels)
        {
            if(includeCarModels)
            {
                return await _context.Companies.Include(company => company.CarModels).Where(company => company.ID == companyID).FirstOrDefaultAsync();
            }
            return await _context.Companies.Where(company => company.ID == companyID).FirstOrDefaultAsync();

        }

        public async Task<bool> DoesCompanyExist(int companyID)
        {
            return await _context.Companies.AnyAsync(company => company.ID == companyID);
        }

        //public async Task<bool> DoesCarModelExist(int carModelID)
        //{

        //    return await _context.CarModels.AnyAsync(carModel => carModel.ID == carModelID);
        //}

        //public async Task<bool> DoesCompanyAndCarModelExist(int companyID, int carModelID)
        //{
        //    var doesCompanyExist = await DoesCompanyExist(companyID);
            
        //}

        public async Task AddCarModelForCompanyAsync(int companyID, CarModel carModel)
        {
            var company = await GetCompanyAsync(companyID, false);
            if (company != null)
            {
                company.CarModels.Add(carModel);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()>0);
        }
    }
}
