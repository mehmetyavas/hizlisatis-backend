using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ISaleOutgoingService
    {
        Task<List<SaleOutgoing>> GetListAsync();
        Task<SaleOutgoing> Get(int id);
        Task<IResult> Add(SaleOutgoingDto saleOutgoing);
        Task<IResult> Update(SaleOutgoing saleOutgoing);
        Task<IResult> Delete(SaleOutgoing saleOutgoing);
    }
}
