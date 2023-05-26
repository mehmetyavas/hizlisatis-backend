using Business.Services.Abstract;
using Business.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<IDataResult<List<TblstsabitDTO>>>> GetBySquery(string? search)
        {
            if (search is null)
            {
                var rawResult = await _stockService.GetTopRecord(100);
                return Ok(new SuccessDataResult<List<TblstsabitDTO>>(rawResult));
            }
            var result = await _stockService.GetByQuery(search);
            return Ok(new SuccessDataResult<List<TblstsabitDTO>>(result.Data));
        }

        [HttpGet("stokKodu")]
        public ActionResult<IDataResult<TblstsabitDTO>> GetById(string? stokKodu)
        {
            if (stokKodu == null)
                throw new Exception("Stok Kodu Bulunamadı");
            var result = _stockService.Get(stokKodu);
            return Ok(new SuccessDataResult<TblstsabitDTO>(result.Data));
        }

    }
}
