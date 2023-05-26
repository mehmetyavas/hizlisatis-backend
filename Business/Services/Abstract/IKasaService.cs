using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Netsis;
using Entities.DTOs.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IKasaService
    {

        List<Tblkasa> GetAll();
        Tblkasa Get(int id);
        Task<IResult> AddSaleToKasa(IEnumerable<Sale> sale);
        Task<IResult> AddOutgoingToKasa(List<SaleOutgoing> outgoings);
        Task<IResult> AddEmployeeOutgoingToKasa(IEnumerable<Sale> sale);

    }
}
