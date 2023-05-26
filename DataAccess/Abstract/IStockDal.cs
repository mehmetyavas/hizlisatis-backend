using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete.Netsis;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStockDal : IEntityRepository<Tblstsabit>
    {
        public Task<IDataResult<List<Tblstsabit>>> GetByQuery(string search);
        public Task<List<Tblstsabit>> GetTop100(int take);
    }
}
