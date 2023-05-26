using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Netsis
{
    public class NetsisReqDTO:IDto
    {
       
        public int Id { get; set; }
        public string? StokKodu { get; set; }
        public string GC { get; set; }
        public decimal GCMik { get; set; }
        public decimal StraNF { get; set; }
        public decimal StraBF { get; set; }
        public double StraKDV { get; set; }
        public string? EkAlan { get; set; }
        public int DepoKodu { get; set; }
       
    }
}
