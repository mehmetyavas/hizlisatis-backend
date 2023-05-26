using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PaymentDTO:IDto
    {
        public string PaymentName { get; set; }
    }
}
