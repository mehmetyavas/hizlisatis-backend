using AutoMapper;
using Business.Services.Abstract;
using Business.Utilities.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class StockManager : IStockService
    {

        IStockDal _stockDal;
        IMapper _mapper;
        public StockManager(IStockDal stockDal, IMapper mapper)
        {
            _stockDal = stockDal;
            _mapper = mapper;
        }

        public IDataResult<TblstsabitDTO> Get(string stockCode)
        {

            var result = _stockDal.Get(x =>
                                x.Barkod1 == stockCode ||
                                x.Barkod2 == stockCode ||
                                x.Barkod3 == stockCode);

            if (result == null)
                throw new Exception("Stok Kodu Bulunamadı");

            var mappedResult = _mapper.Map<TblstsabitDTO>(result);

            return new SuccessDataResult<TblstsabitDTO>(mappedResult);
        }
        public async Task<IDataResult<List<TblstsabitDTO>>> GetByQuery(string search)
        {
            string searchSql = null!;

            if (search.Contains(' '))
            {
                var searchWord = search.Split(' ');
                foreach (var item in searchWord)
                    searchSql += item + '%';
            }
            else
                searchSql = search + '%';

            var result = await _stockDal.GetByQuery(StringValidations.StringCheckReverse(searchSql.ToUpper()));

            var mappedResult = _mapper.Map<List<TblstsabitDTO>>(result.Data);

            return new SuccessDataResult<List<TblstsabitDTO>>(mappedResult);
        }

        public async Task<List<TblstsabitDTO>> GetTopRecord(int take)
        {

            var result = await _stockDal.GetTop100(take);

            var mappedResult = _mapper.Map<List<TblstsabitDTO>>(result);
            return new List<TblstsabitDTO>(mappedResult);
        }






    }
}
