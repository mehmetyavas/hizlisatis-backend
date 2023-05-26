using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Sale
{
    public class SaleSlipReqDTO:IDto
    {

        public string stockName { get; set; }
        public decimal amount { get; set; }
        public decimal price { get; set; }
        public string VATGroup { get; set; }

    }
}
