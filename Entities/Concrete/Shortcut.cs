using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Shortcut:IEntity
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int TabIndex { get; set; }
        public string StockCode { get; set; }
        public string? StockName { get; set; } = null;
        public string? Title { get; set; } = null;
        public DateTime? CreatedAt { get; set; }=default(DateTime?);
        public DateTime? UpdatedAt { get; set; } = null;

    }
}
