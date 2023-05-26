using Core.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Sale : IEntity
    {
        public int Id { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Payment")]
        public int PaymentType { get; set; }
        public virtual Payment? Payment { get; set; }

        public string VATGroup { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerDescription { get; set; }
        public bool? Transfer { get; set; } = false;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public Shift? Shift { get; set; }

        public int SlipId { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;





    }
}
