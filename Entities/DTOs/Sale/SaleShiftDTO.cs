using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Sale
{
    public class SaleShiftDTO:IDto
    {
        public int id { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string VATGroup { get; set; }
        public bool? Transfer { get; set; }
        public int ShiftId { get; set; }
        public bool IsVATIncluded { get; set; }
        public int paymentType { get; set; }
        public int SlipId { get; set; }
        public int? EmployeeId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
