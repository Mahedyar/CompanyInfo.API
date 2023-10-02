using CompanyInfo.API.Repositories;


namespace CompanyInfo.API.Services
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ValidateService
    {
        private readonly ICompanyInfoRepository _companyInfoRepository;
        private readonly ILogger<ValidateService> _logger;


        public ValidateService(ICompanyInfoRepository companyInfoRepository, ILogger<ValidateService> logger)
        {
            this._companyInfoRepository = companyInfoRepository;
            this._logger = logger;
        }

        public async Task<ValidationResult> ValidateCompanyAndCarModel(int companyID, int? carModelID)
        {
            if (!await _companyInfoRepository.DoesCompanyExist(companyID))
            {
                _logger.LogInformation($"We Dont Have Such a Company With This ID : {companyID}");
                //return NotFound();
                return new ValidationResult { IsValid = false, ErrorMessage = $"Company with ID {companyID} not found." };
            }
            if (carModelID.HasValue)
            {
                var carModel = await _companyInfoRepository.GetSingleCarModelForCompanyAsync(companyID, carModelID.Value);
                if (carModel == null)
                {
                    _logger.LogInformation($"We Dont Have Such a Carmodel With This ID : {carModelID} In The Company");
                    return new ValidationResult { IsValid = false, ErrorMessage = $"We Dont Have Such a Carmodel With This ID : {carModelID} In The Company" };
                }
            }
            return new ValidationResult { IsValid = true };
        }



    }
}
