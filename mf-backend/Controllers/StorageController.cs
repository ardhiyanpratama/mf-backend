using instaapp_backend.Core.IConfiguration;
using mf_backend.Dto;
using mf_backend.Helper;
using mf_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace mf_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BpkbController> _logger;

        public StorageController(IUnitOfWork unitOfWork, ILogger<BpkbController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MsStorageLocationDto msStorageLocationDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequestResponse(ResponseMessageExtensions.Database.DATA_NOT_VALID);
            }

            var newPosting = new MsStorageLocation
            {
                LocationId = msStorageLocationDto.LocationId,
                LocationName = msStorageLocationDto.LocationName
            };

            var isSuccess = await _unitOfWork.Storage.AddAsync(newPosting);
            if (!isSuccess)
            {
                return this.BadRequestResponse(ResponseMessageExtensions.SYSTEM_ERROR, errorcode: (long)ErrorCode.SystemError);
            }

            await _unitOfWork.CompleteAsync();

            return this.OkResponse(ResponseMessageExtensions.Database.WRITE_SUCCESS);
        }

        [HttpGet]
        public async Task<IActionResult> GetStorage()
        {
            var isSuccess = await _unitOfWork.Storage.GetAllAsync();
            if (isSuccess.Count() == 0)
            {
                return this.BadRequestResponse(ResponseMessageExtensions.SYSTEM_ERROR, errorcode: (long)ErrorCode.SystemError);
            }

            return this.OkResponse(ResponseMessageExtensions.SUCCESS_HEADER, data:isSuccess);
        }

    }
}
