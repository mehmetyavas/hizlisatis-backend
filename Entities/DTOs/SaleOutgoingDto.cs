using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SaleOutgoingDto:IDto
    {
        public string Description { get; set; } = null!;
        public int OutgoingId { get; set; }
        public int ShiftId { get; set; }
        public bool Transfer { get; set; } = false;
        public decimal Price { get; set; }
        public string? Receiver { get; set; }
        public string? Submitter { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
