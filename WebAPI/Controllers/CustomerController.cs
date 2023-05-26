using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("code")]
        public async Task<IDataResult<TblcasabitDTO>> Get(string customerCode)
        {
            return new SuccessDataResult<TblcasabitDTO>(await _customerService.Get(customerCode));
        }
        [HttpGet]
        public async Task<IDataResult<List<TblcasabitDTO>>> GetListByCustomer(string? search)
        {
            return new SuccessDataResult<List<TblcasabitDTO>>(await _customerService.GetByQuery(search));
        }
    }
}
