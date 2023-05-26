using Business.Services.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {

        IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }




        [HttpGet]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_service.GetAllRole());
        }

        [HttpGet("{id}")]
        public IDataResult<OperationClaim> Get(int id)
        {
            return new SuccessDataResult<OperationClaim>(_service.GetRole(id));
        }

        [HttpPost("add")]
        public IResult Add(OperationClaim role)
        {
            var result = _service.AddRole(role);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);

        }

        [HttpPost("update")]
        public IResult Update(OperationClaim role)
        {

            var result = _service.UpdateRole(role);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);

        }

        [HttpPost("delete")]
        public IResult Delete(OperationClaim role)
        {
            var result = _service.DeleteRole(role);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);

        }

    }
}
