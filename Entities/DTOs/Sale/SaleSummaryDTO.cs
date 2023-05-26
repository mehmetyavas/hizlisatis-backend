using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Sale
{
    public class SaleSummaryDTO:IDto
    {
        public int id { get; set; }
        public string stockCode { get; set; }
        public string stockName { get; set; }
        public decimal amount { get; set; }
        public decimal price { get; set; }
        public string payment { get; set; }
        public string VATGroup { get; set; }
        public string customerCode { get; set; }
        public bool? transfer { get; set; }
        public string user { get; set; }
        public string shift { get; set; }
        public int slipId { get; set; }
        public string? employee { get; set; }
        public DateTime? createdAt { get; set; }
        public string? CustomerDescription { get; set; }
    }
}
