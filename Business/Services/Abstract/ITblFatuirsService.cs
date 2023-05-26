using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ITblFatuirsService
    {
        List<Tblfatuir> GetByFatNo(string fatNo);
        List<Tblfatuir> GetByCustomerCode(string customerCode);
        Task<string> GetMaxFatByFatNo(string ftIrsip, string fatNo);

        Task<string> getIdentifier(string cari);
    }
}
