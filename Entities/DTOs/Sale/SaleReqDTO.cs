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
    public class SaleReqDTO : IDto
    {
        public string? DocumentType { get; set; }
        public int ShiftId { get; set; }
        public bool IsVATIncluded { get; set; }
        public string? TeslimAlan { get; set; }
        public string? TeslimEden { get; set; }
        public int paymentType { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public int SlipId { get; set; }
        public int? EmployeeId { get; set; }

        public int UserId { get; set; }

        public List<ReqListDTO> Sales { get; set; } = null!;

    }
}
