using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Sale
{
    public class SaleSlipDTO:IDto
    {
        public string payment { get; set; }
        public string User { get; set; }
        public int slipId { get; set; }

        public string? CustomerName { get; set; }
        public string? Receiver { get; set; }
        public string? Delivery { get; set; }
        public DateTime? createdAt { get; set; }

        public List<SaleSlipReqDTO> Sales { get; set; } = null!;
    }
}
