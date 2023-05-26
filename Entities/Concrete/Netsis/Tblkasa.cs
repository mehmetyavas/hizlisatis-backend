using Core.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblkasa : IEntity
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

    public decimal? Doviztut { get; set; }

    public decimal? Kur { get; set; }

    public string? PlasiyerKodu { get; set; }

    public int? Odemeturu { get; set; }

    public string? UpdateKodu { get; set; }

    public string? Zaman { get; set; }

    public short? KullId { get; set; }

    public int Sira { get; set; }

    public short SubeKodu { get; set; }

    public string? KrediKartNo { get; set; }

    public decimal? KdvOran { get; set; }

    public string? KdvDahil { get; set; }

    public string? Yedek1 { get; set; }

    public string? Yedek2 { get; set; }

    public decimal? Yedek3 { get; set; }

    public decimal? Yedek4 { get; set; }

    public decimal? Yedek5 { get; set; }

    public string? Yedek6 { get; set; }

    public byte? Yedek7 { get; set; }

    public short? Yedek8 { get; set; }

    public int? Yedek9 { get; set; }

    public DateTime? Yedek10 { get; set; }

    public string? ProjeKodu { get; set; }

    public string? Kayityapankul { get; set; }

    public DateTime? Kayittarihi { get; set; }

    public string? Duzeltmeyapankul { get; set; }

    public DateTime? Duzeltmetarihi { get; set; }

    public string Onaytipi { get; set; } = null!;

    public int Onaynum { get; set; }

    public byte? Kaynak { get; set; }

    public string? Entegrefkey { get; set; }

    public int? Krtsozmasinckeyno { get; set; }

    public short? Taksit { get; set; }

    public string? Gecerli { get; set; }

    public string? Bformcarikodu { get; set; }

    public string? Refkod { get; set; }

    public string? Indkdv { get; set; }
}
