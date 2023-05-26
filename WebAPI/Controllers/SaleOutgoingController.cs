using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/sale-outgoing")]
    [ApiController]
    public class SaleOutgoingController : ControllerBase
    {


        ISaleOutgoingService _saleOutgoingService;

        public SaleOutgoingController(ISaleOutgoingService saleOutgoingService)
        {
            _saleOutgoingService = saleOutgoingService;
        }

        [HttpPost("add")]
        public async Task<IResult> Post(SaleOutgoingDto outgoing)
        {
            var result = await _saleOutgoingService.Add(outgoing);

            return new SuccessResult(result.Message);
        }


        [HttpPost("delete")]
        public async Task<IResult> Post(int id)
        {
            var recordToDelete = await _saleOutgoingService.Get(id);


            var result = await _saleOutgoingService.Delete(recordToDelete);

            return new SuccessResult(result.Message);
        }

        [HttpGet]
        public async Task<IDataResult<List<SaleOutgoing>>> GetListAsync()
        {
            var result = await _saleOutgoingService.GetListAsync();

            return new SuccessDataResult<List<SaleOutgoing>>(result);
        }


    }
}
