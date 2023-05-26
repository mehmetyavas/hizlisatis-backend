using Business.Services.Abstract;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class TblFatuirsManager : ITblFatuirsService
    {
        IFatuirsDal _fatuirsDal;

        public TblFatuirsManager(IFatuirsDal fatuirsDal)
        {
            _fatuirsDal = fatuirsDal;
        }

        public List<Tblfatuir> GetByCustomerCode(string customerCode)
        {
            return _fatuirsDal.GetAll(x => x.CariKodu == customerCode);
        }

        public List<Tblfatuir> GetByFatNo(string fatNo)
        {
            return _fatuirsDal.GetAll(x => x.FatirsNo == fatNo);
        }

        public async Task<string> getIdentifier(string cari)
        {
            return await _fatuirsDal.Identifier(cari);
        }

        public async Task<string> GetMaxFatByFatNo(string ftIrsip, string fatNo)
        {
            return await _fatuirsDal.GetMaxFat(ftIrsip, fatNo);  
        }
    }
}
