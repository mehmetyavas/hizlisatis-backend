using Core.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblefatcari:IEntity
{
    public string Identifier { get; set; } = null!;

    public string? Alias { get; set; }

    public string? Title { get; set; }

    public string? Efattype { get; set; }

    public DateTime? Tarih { get; set; }

    public byte? Profileid { get; set; }

    public string? UpdateKodu { get; set; }

    public string Aktif { get; set; } = null!;

    public string? CYedek1 { get; set; }

    public string? CYedek2 { get; set; }

    public int? IYedek3 { get; set; }

    public int? IYedek4 { get; set; }

    public int? IYedek5 { get; set; }

    public int? IYedek6 { get; set; }

    public double? FYedek5 { get; set; }

    public double? FYedek7 { get; set; }

    public double? FYedek8 { get; set; }

    public double? FYedek6 { get; set; }

    public DateTime? DYedek7 { get; set; }

    public DateTime? DYedek8 { get; set; }

    public DateTime? DYedek9 { get; set; }

    public DateTime? DYedek10 { get; set; }
}
