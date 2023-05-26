using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/outgoing")]
    [ApiController]
    public class OutgoingController : ControllerBase
    {

        IOutgoingService _outgoingService;

        public OutgoingController(IOutgoingService outgoingService)
        {
            _outgoingService = outgoingService;
        }

        [HttpGet]
        public async Task<IDataResult<List<Outgoing>>> GetOutgoingsAsync()
        {
            var result = await _outgoingService.GetAllAsync();
            return new SuccessDataResult<List<Outgoing>>(result);
        }
        [HttpGet("{id}")]
        public async Task<IDataResult<Outgoing>> GetOutgoingAsync(int id)
        {
            var result = await _outgoingService.Get(id);
            return new SuccessDataResult<Outgoing>(result);
        }


        [HttpPost("add")]
        public async Task<IDataResult<Outgoing>> AddOutgoingAsync(Outgoing req)
        {
            var result = await _outgoingService.Add(req);
            return result;
        }

        [HttpPost("delete")]
        public async Task<Core.Utilities.Results.IResult> deleteOutgoingAsync(int id)
        {
            var record = await _outgoingService.Get(id);
            var result = await _outgoingService.Delete(record);
            return result;
        }

        [HttpPost("update")]
        public async Task<Core.Utilities.Results.IResult> updateOutgoingAsync(Outgoing req)
        {
            var result = await _outgoingService.UpdateAsync(req);
            return result;
        }
    }
}
