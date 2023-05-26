using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class TblstsabitDTO:IDto
    {
        public string StokKodu { get; set; } = null!;
        public string? StokAdi { get; set; }
        public string? GrupKodu { get; set; }
        public string? Kod1 { get; set; }
        public string? Kod2 { get; set; }
        public string? Kod3 { get; set; }
        public string? Kod4 { get; set; }
        public string? Kod5 { get; set; }
        public string? OlcuBr1 { get; set; }
        public decimal? SatisFiat1 { get; set; }
        public decimal? SatisFiat4 { get; set; }
        public int? MuhDetaykodu { get; set; }
        public decimal? KdvOrani { get; set; }
        public short? DepoKodu { get; set; }
        public string? OncekiKod { get; set; }
        public string? SonrakiKod { get; set; }
        public string? Barkod1 { get; set; }
        public string? Barkod2 { get; set; }
        public string? Barkod3 { get; set; }
        public decimal? AlisKdvKodu { get; set; }
        public decimal? AlisFiat1 { get; set; }
        public decimal? AlisFiat2 { get; set; }
        public decimal? AlisFiat3 { get; set; }
        public decimal? AlisFiat4 { get; set; }

    }
}
