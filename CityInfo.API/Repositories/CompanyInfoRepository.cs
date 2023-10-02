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


        public async Task AddCarModelForCompanyAsync(int companyID, CarModel carModel)
        {
            var company = await GetCompanyAsync(companyID, false);
            if (company != null)
            {
                company.CarModels.Add(carModel);
            }
        }


        public async Task EditCarModelInCompanyAsync(int companyID, int carModelID, CarModel carModel)
        {
            var company = await GetCompanyAsync(companyID, true);
            if(company != null)
            {
                var oldCarModel = company.CarModels.FirstOrDefault(carModel => carModel.ID == carModelID);
                oldCarModel.Model = carModel.Model;
                oldCarModel.Description = carModel.Description;
                oldCarModel.CompanyID = companyID;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()>0);
        }

    }
}
