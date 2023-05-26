using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Netsis
{
    public class SendSaleToNetsisDTO:IDto
    {
        public object? DocumentType { get; set; }
        public int ShiftId { get; set; }
        public bool IsVATIncluded { get; set; } = true;
        public string? TeslimAlan { get; set; }
        public string? TeslimEden { get; set; }
        public string? fatNo { get; set; }
        public int? paymentType { get; set; }
        public string? Cari { get; set; }
        public List<NetsisReqDTO> NetsisReqs { get; set; } = null!;
      

    }
}
