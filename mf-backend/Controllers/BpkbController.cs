using AutoMapper;
using HashidsNet;
using instaapp_backend.Core.IConfiguration;
using mf_backend.Dto;
using mf_backend.Helper;
using mf_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mf_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BpkbController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BpkbController> _logger;
        public BpkbController(IUnitOfWork unitOfWork, ILogger<BpkbController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrBpkbDto trBpkbDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequestResponse(ResponseMessageExtensions.Database.DATA_NOT_VALID);
            }

            var newPosting = new TrBpkb
            {
                AgreementNumber = trBpkbDto.AgreementNumber,
                BpkbNo = trBpkbDto.BpkbNo,
                BpkbDate = trBpkbDto.BpkbDate,
                BranchId = trBpkbDto.BranchId,
                FakturDate = trBpkbDto.FakturDate,
                FakturNo = trBpkbDto.FakturNo,
                PoliceNo = trBpkbDto.PoliceNo,
                BpkbDateIn = trBpkbDto.BpkbDateIn
            };

            var isSuccess = await _unitOfWork.Bpkb.AddAsync(newPosting);
            if (!isSuccess)
            {
                return this.BadRequestResponse(ResponseMessageExtensions.SYSTEM_ERROR, errorcode: (long)ErrorCode.SystemError);
            }

            await _unitOfWork.CompleteAsync();

            return this.OkResponse(ResponseMessageExtensions.Database.WRITE_SUCCESS);
        }
    }
}
