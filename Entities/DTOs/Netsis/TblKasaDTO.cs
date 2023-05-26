using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Netsis
{
    public class TblKasaDTO:IDto
    {
        public string KsmasKod { get; set; } = null!;

        public DateTime Tarih { get; set; }

        public string? Fisno { get; set; }

        public string Io { get; set; } = null!;

        public string Tip { get; set; } = null!;

        public string? Aciklama { get; set; }

        public decimal? Tutar { get; set; }

        public string? CariMuh { get; set; }

        public string? Kod { get; set; }

        public string? PlasiyerKodu { get; set; }

        public short SubeKodu { get; set; }

    }
}
