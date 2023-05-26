using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Netsis
{
    public class NetsisDTOs
    {
        ISettingDal _settingDal;

        public NetsisDTOs(ISettingDal settingDal)
        {
            _settingDal = settingDal;
        }

        public NetsisReqDTO GetSendOutgoingSales(
          SaleOutgoing outGoingCustomerSales)
        {
            NetsisReqDTO sendSaleToNetsisDTOs = new NetsisReqDTO();

            sendSaleToNetsisDTOs = new NetsisReqDTO
            {
                DepoKodu = int.Parse(_settingDal.Get(x => x.Key == Settings.depoKodu).Value),
                GC = "C",
                GCMik = 1,
                StraNF = outGoingCustomerSales.Price,
                StraBF = outGoingCustomerSales.Price,
                Id = outGoingCustomerSales.Id
            };
            return sendSaleToNetsisDTOs;
        }
    }
}
