using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/shift")]
    [ApiController]
    public class ShiftController : ControllerBase
    {

        IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }


        [HttpGet]
        public IDataResult<List<Shift>> GetAll()
        {
            var result = _shiftService.GetAll();
            return new SuccessDataResult<List<Shift>>(result);
        }

        [HttpGet("{id}")]
        public IDataResult<Shift> Get(int id)
        {
            var result = _shiftService.GetById(id);
            return new SuccessDataResult<Shift>(result);
        }


        [HttpPost("add")]
        public IResult Add(Shift shift)
        {
            var result = _shiftService.Add(shift);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
        }

        [HttpPost("update")]
        public IResult Update(Shift shift)
        {
            var result = _shiftService.Update(shift);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
        }

        [HttpPost("delete/{id}")]
        public IResult Delete(int id)
        {
            var recordToDelete = _shiftService.GetById(id);

            var result = _shiftService.Delete(recordToDelete);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
        }
    }
}
