using Core.Abstract;
using Core.Utilities.Results;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IStockService
    {
        Task<IDataResult<List<TblstsabitDTO>>> GetByQuery(string search);
        IDataResult<TblstsabitDTO> Get(string stockCode);

        Task<List<TblstsabitDTO>> GetTopRecord(int take);
    }
}
