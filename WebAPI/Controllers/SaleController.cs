using AutoMapper;
using Business.Abstract;
using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Sale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Claims;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/sale")]
    [ApiController]
    //[Authorize]
    public class SaleController : ControllerBase
    {
        ISaleService _saleService;
        private readonly IMapper _mapper;
        ISettingService _settingService;
        public SaleController(ISaleService saleService, IMapper mapper, ISettingService settingService)
        {
            _saleService = saleService;
            _mapper = mapper;
            _settingService = settingService;
        }



        [HttpGet]
        public async Task<IDataResult<IEnumerable<SaleDTO>>> GetAsync()
        {
            var result = await _saleService.GetAll();

            return result;

        }

        [HttpGet("slip")]
        public async Task<IDataResult<IEnumerable<SaleDTO>>> GetSlipAsync(int id)
        {
            return await _saleService.GetSlipByDetail(id);
        }



        [HttpPost("add")]
        public async Task<IDataResult<SaleSlipDTO>> Post(SaleReqDTO req)
        {
            //if (!User.Identity.IsAuthenticated)
            //    throw new Exception();

            //var deneme = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            //if (deneme == null)
            //    throw new Exception();

            //foreach (var sale in req)
            //{
            //    sale.UserId = Convert.ToInt32(deneme);
            //}
            var AddedRecord = await _saleService.Add(req);

            return AddedRecord.Success
            ? req.paymentType != 5
                ? await _saleService.GetSlipByMaxSlipIdAsync(req)
                : null!
            : throw new Exception();

        }

        [HttpPost("add-by-shift")]
        public async Task<IResult> AddToNetsis(int shift)
        {
            List<SaleShiftDTO> req = new List<SaleShiftDTO>();

            var getShiftSlips = await _saleService.getByShift(shift);

            if (getShiftSlips.Data.Count() == 0)
                throw new Exception();

            var mappedResult = _mapper.Map<List<SaleShiftDTO>>(getShiftSlips.Data);

            req = mappedResult;

            var result = await _saleService.AddByShift(req);

            return result.Success ? new SuccessResult(result.Message + "kayıt Başarılı!") : throw new Exception();
        }

        [HttpPost("update")]
        public async Task<IResult> UpdateAsync(SaleReqDTO req)
        {
            var mappedResult = _mapper.Map<Sale>(req);
            var result = await _saleService.UpdateSaleAsync(mappedResult);

            return result.Success ? new SuccessResult("Satış Düzenlendi") : throw new Exception();
        }

        [HttpPost("delete/{id}")]
        public async Task<IResult> Delete(int id)
        {
            var recordToDelete = _saleService.Get(id).Data;
            var result = await _saleService.DeleteSaleAsync(recordToDelete);
            return result.Success ? new SuccessResult("Silme işlemi başarılı!") : throw new Exception();
        }

        [HttpGet("sale-summary")]
        public async Task<IDataResult<IEnumerable<SaleSummaryDTO>>> GetSummaryAsync(DateTime? date, int userId)
        {
            return await _saleService.GetSaleSummariesAsync(date, userId);
        }
    }
}
