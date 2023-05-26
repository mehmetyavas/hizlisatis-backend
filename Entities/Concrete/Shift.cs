using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Shift:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
