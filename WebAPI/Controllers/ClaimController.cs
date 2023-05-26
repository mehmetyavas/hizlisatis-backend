using Business.Abstract;
using Business.Services.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/claim")]
    [ApiController]
    [Authorize]
    public class ClaimController : ControllerBase
    {


        IUserOperationClaimService _service;

        public ClaimController(IUserOperationClaimService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IDataResult<IEnumerable<UserOperationClaim>>> GetAllUserClaim()
        {
            return new SuccessDataResult<IEnumerable<UserOperationClaim>>(await _service.GetAllUserClaim());
        }

        [HttpGet("user/{id}")]
        public IDataResult<List<UserOperationClaim>> GetClaimByUserId(int id)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_service.GetClaimByUserId(id));
        }

        [HttpGet("{id}")]

        public IDataResult<UserOperationClaim> GetClaim(int id)
        {
            var claim = _service.GetClaim(id);
            return new SuccessDataResult<UserOperationClaim>(claim);
        }

        [HttpPost("add")]
        public IResult AddClaim(UserOperationClaim claim)
        {
            var result = _service.AddClaim(claim);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
        }

        [HttpPost("update")]
        public IResult UpdateClaim(UserOperationClaim claim)
        {
            var result = _service.UpdateClaim(claim);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
        }

        [HttpPost("delete")]
        public IResult DeleteClaim(int id)
        {
            var recordToDelete = _service.GetClaim(id);
            var result = _service.DeleteClaim(recordToDelete);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
        }



    }
}
