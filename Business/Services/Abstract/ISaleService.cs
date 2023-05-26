using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISaleService
    {
        Task<IDataResult<IEnumerable<SaleDTO>>> GetAll();
        Task<IDataResult<IEnumerable<SaleDTO>>> GetSlipByDetail(int slipId);
        Task<IDataResult<SaleSlipDTO>> GetSlipByMaxSlipIdAsync(SaleReqDTO req);
        Task<IDataResult<IEnumerable<Sale>>> getByShift(int shift);
        IDataResult<Sale> Get(int id);
        Task<IDataResult<IEnumerable<SaleSummaryDTO>>> GetSaleSummariesAsync(DateTime? date, int userId);

        Task<IResult> Add(SaleReqDTO sale);
        Task<IResult> AddByShift(List<SaleShiftDTO> sale);
        Task<IResult> UpdateSaleAsync(Sale sale);
        Task<IResult> DeleteSaleAsync(Sale sale);
    }
}
