using AutoMapper;
using Business.Services.Abstract;
using Business.Utilities.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal, IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
        }

        public async Task<TblcasabitDTO> Get(string CustomerCode)
        {
            var result = await _customerDal.GetAsync(c => c.CariKod == CustomerCode);

            var mappedResult = _mapper.Map<TblcasabitDTO>(result);

            return mappedResult;
        }
        public async Task<List<TblcasabitDTO>> GetByQuery(string search)
        {
            //var result = _customerDal.GetAll(x => x.CariIsim.Contains(search)).ToList();
            var searchResult = search != null ?  search:"";
            var result = await _customerDal.GetCustomerByTurkishChar(StringValidations.StringCheckReverse(searchResult.ToUpper()));
            var mappedResult = _mapper.Map<List<TblcasabitDTO>>(result);
            return mappedResult;


        }
    }
}
