using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IDataResult<List<Employee>>> GetAll()
        {
            var result = await _service.GetEmployeeListAsync();
            return new SuccessDataResult<List<Employee>>(result.ToList());
        }

        [HttpGet("id")]
        public IDataResult<Employee> GetById(int id)
        {
            return new SuccessDataResult<Employee>(_service.GetEmployeeById(id));
        }

        [HttpGet("name")]
        public IDataResult<Employee> GetByName(string name)
        {
            return new SuccessDataResult<Employee>(_service.GetEmployeeByName(name));
        }
        [HttpPost("add")]
        public async Task<IDataResult<Employee>> Add(Employee req)
        {
            var result = await _service.AddEmployeeAsync(req);
            return result;
        }

        [HttpPost("delete")]
        public async Task<IResult> Delete(EmployeeDeleteDTO request)
        {
            var req = _service.GetEmployeeById(request.Id);
            await _service.DeleteAsync(req);
            return new SuccessResult("Silindi!");
        }
        [HttpPost("update")]
        public async Task<IDataResult<Employee>> Update(Employee req)
        {
            var result = await _service.UpdateEmployeeAsync(req);
            return result;
        }

    }
}
