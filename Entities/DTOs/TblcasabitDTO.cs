using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class TblcasabitDTO:IDto
    {

        public string CariKod { get; set; } = null!;

        public string? CariIsim { get; set; }
    }
}
