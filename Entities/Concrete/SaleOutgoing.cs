using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SaleOutgoing:IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int OutgoingId { get; set; }
        public Outgoing? Outgoing { get; set; }
        public int ShiftId { get; set; }
        public Shift? Shift { get; set; }
        public bool Transfer { get; set; } = false;
        public decimal Price { get; set; }
        public string Receiver { get; set; }
        public string Submitter { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
    }
}
