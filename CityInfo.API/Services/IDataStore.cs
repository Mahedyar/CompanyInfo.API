using CompanyInfo.API.Models;

namespace CompanyInfo.API.Services
{
    public interface IDataStore
    {
        List<CompanyDto> Companies { get; set; }
    }
}