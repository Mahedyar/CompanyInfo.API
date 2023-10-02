using AutoMapper;
using CompanyInfo.API.Entities;
using CompanyInfo.API.Models;
using CompanyInfo.API.Repositories;
using CompanyInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.API.Controllers
{
    [Route("api/Companies/{companyID}/Carmodels")]
    [ApiController]
    public class CarModelsController : ControllerBase
    {
        private readonly ILogger<CarModelsController> _logger;
        private readonly IMailService _localMailService;
        private readonly IDataStore _dataStore;
        private readonly IMapper _mapper;
        private readonly ICompanyInfoRepository _companyInfoRepository;
        private readonly ValidateService _validateService;

      

        public CarModelsController(ILogger<CarModelsController> logger, IMailService localMailService, IDataStore dataStore, IMapper mapper, ICompanyInfoRepository companyInfoRepository, ValidateService validateService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(_dataStore));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._companyInfoRepository = companyInfoRepository ?? throw new ArgumentNullException(nameof(_companyInfoRepository));
            this._validateService = validateService;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModelDto>>> GetCarModels(int companyID)
        {
            var validationResult = await _validateService.ValidateCompanyAndCarModel(companyID, null);
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }
            var carModels = await _companyInfoRepository.GetCarModelsForCompanyAsync(companyID);
            return Ok(_mapper.Map<IEnumerable<CarModelDto>>(carModels));

        }
        [HttpGet("{carModelID}", Name = "GetCarModel")]
        public async Task<ActionResult<CarModelDto>> GetCarModel(int companyID, int carModelID)
        {
            var validationResult = await _validateService.ValidateCompanyAndCarModel(companyID, carModelID);
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }
            var carModel = await _companyInfoRepository.GetSingleCarModelForCompanyAsync(companyID, carModelID);
            return Ok(_mapper.Map<CarModelDto>(carModel));
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<CarModelDto>> AddCarModel(int companyID, CarModelCreationDto carModel)
        {
            var validationResult = await _validateService.ValidateCompanyAndCarModel(companyID, null);
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }
            var carModelEntityForm = _mapper.Map<Entities.CarModel>(carModel);
            await _companyInfoRepository.AddCarModelForCompanyAsync(companyID, carModelEntityForm);
            await _companyInfoRepository.SaveChangesAsync();
            var carModelDtoForm = _mapper.Map<Models.CarModelDto>(carModelEntityForm);
            //hello world
            return CreatedAtRoute("GetCarModel", new
            {
                companyID = companyID,
                carmodelID = carModelDtoForm.ID
            }, carModelDtoForm);
        }
        #endregion

        #region Edit
        [HttpPut("{carModelID}")]

        public async Task<ActionResult> UpdateCarModel(int companyID, int carModelID, CarModelUpdateDto inputCarModel)
        {
            var validationResult = await _validateService.ValidateCompanyAndCarModel(companyID, carModelID);
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }
            


            var oldCarModel = await _companyInfoRepository.GetSingleCarModelForCompanyAsync(companyID, carModelID);
            _mapper.Map(inputCarModel, oldCarModel);
            
            //var carModelEntityForm = _mapper.Map<Entities.CarModel>(inputCarModel);
            //await _companyInfoRepository.EditCarModelInCompanyAsync(companyID, carModelID, carModelEntityForm);

            var carModelDtoForm = _mapper.Map<Models.CarModelDto>(oldCarModel);
            await _companyInfoRepository.SaveChangesAsync();
            return CreatedAtRoute("GetCarModel", new { companyID = companyID, carModelID = carModelID }, carModelDtoForm);
            
            //return NoContent();



        }


        #endregion

        #region Edit with Patch
        [HttpPatch("{carModelID}")]
        public async Task<ActionResult> PartiallyUpdateCarModel(
            int companyID, int carModelID, JsonPatchDocument<CarModelUpdateDto> patchDocument
            )
        {
            var validationResult = await _validateService.ValidateCompanyAndCarModel(companyID, carModelID);
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }

            var oldCarModel = await _companyInfoRepository.GetSingleCarModelForCompanyAsync(companyID, carModelID);
            var carModelUpdateDtoForm = _mapper.Map<CarModelUpdateDto>(oldCarModel);
            patchDocument.ApplyTo(carModelUpdateDtoForm, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(carModelUpdateDtoForm))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(carModelUpdateDtoForm, oldCarModel);
            await _companyInfoRepository.SaveChangesAsync();
            return NoContent();
            
            
        }
        #endregion

        #region Delete

        [HttpDelete("{carModelID}")]
        public async Task<ActionResult>DeleteCarModel(int companyID, int carModelID)
        {
            var validationResult = await _validateService.ValidateCompanyAndCarModel(companyID, carModelID);
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }

            var carModel = await _companyInfoRepository.GetSingleCarModelForCompanyAsync(companyID, carModelID);

            _companyInfoRepository.DeleteCarModel(carModel);
            _companyInfoRepository.SaveChangesAsync();

            _localMailService.SendMail("Carmodel Deleted", $"CarModel Named {carModel.Model} with ID:{carModel.ID} is Deleted");

            return NoContent();

        }


        #endregion

    }
}
