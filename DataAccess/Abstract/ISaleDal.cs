using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISaleDal : IEntityRepository<Sale>
    {

        Task<IEnumerable<SaleDTO>> GetSaleDetail(Expression<Func<SaleDTO, bool>> expression = null);
        public Task<IResult> AddSlip(List<Sale> sale);

        Task<int> GetMaxSlipNo();

        Task<IEnumerable<SaleSummaryDTO>> GetSaleSummaries(DateTime? date, int userId);

    }
}
