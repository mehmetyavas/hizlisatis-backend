using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Netsis
{
    [Keyless]
    public class EfatCariVw:IEntity
    {
        public string Identifier { get; set; }
        public string CariKod { get; set; }
    }
}
