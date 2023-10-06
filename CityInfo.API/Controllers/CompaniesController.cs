using AutoMapper;
using CompanyInfo.API.Models;
using CompanyInfo.API.Repositories;
using CompanyInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Companies")]

    public class CompaniesController : ControllerBase
    {
        private readonly IDataStore _dataStore;
        private readonly ICompanyInfoRepository _companyInfoRepository;
        private readonly IMapper _mapper;

        public CompaniesController(IDataStore dataStore , ICompanyInfoRepository companyInfoRepository , IMapper mapper)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            this._companyInfoRepository = companyInfoRepository ?? throw new ArgumentNullException(nameof(companyInfoRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyWithoutCarModelDto>>> GetCompanies()
        {
            var companies = await _companyInfoRepository.GetCompaniesAsync();

            var result = _mapper.Map<IEnumerable<CompanyWithoutCarModelDto>>(companies);

            return Ok(result);
            
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetCompany(int ID, bool includeCarModels=false)
        {
            var company =await _companyInfoRepository.GetCompanyAsync(ID, includeCarModels);
            if(company == null)
            {
                return NotFound();
            }
            if (includeCarModels)
            {
                return Ok(_mapper.Map<CompanyDto>(company));
            }
            return Ok(_mapper.Map<CompanyWithoutCarModelDto>(company));
            
        }
    }
}
