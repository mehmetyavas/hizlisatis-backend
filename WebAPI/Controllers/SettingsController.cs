using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {


        ISettingService _service;

        public SettingsController(ISettingService service)
        {
            _service = service;
        }



        [HttpGet]
        public IDataResult<Dictionary<string, string>> GetAll()
        {
            var result = _service.GetAll();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in result)
                dict.Add(item.Key, item.Value);

            return new SuccessDataResult<Dictionary<string, string>>(dict);
        }

        [HttpPost("update")]
        public IResult Update(Dictionary<string, string> req)
        {
            var result = _service.Update(req);
            return result.Success
                ? new SuccessResult(result.Message)
                : throw new Exception(result.Message);
            return null;

        }


    }
}
