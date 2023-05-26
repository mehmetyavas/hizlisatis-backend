using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblfatuir : IEntity
{
    public short SubeKodu { get; set; }

    public string Ftirsip { get; set; } = null!;

    public string FatirsNo { get; set; } = null!;

    public string CariKodu { get; set; } = null!;

    public DateTime Tarih { get; set; }

    public byte? Tipi { get; set; }

    public decimal? Bruttutar { get; set; }

    public decimal? SatIskt { get; set; }

    public decimal? MfazIskt { get; set; }

    public decimal? GenIsk1t { get; set; }

    public decimal? GenIsk2t { get; set; }

    public decimal? GenIsk3t { get; set; }

    public decimal? GenIsk1o { get; set; }

    public decimal? GenIsk2o { get; set; }

    public decimal? GenIsk3o { get; set; }

    public decimal? Kdv { get; set; }

    public decimal? FatAltm1 { get; set; }

    public decimal? FatAltm2 { get; set; }

    public string? Aciklama { get; set; }

    public string? Kod1 { get; set; }

    public string? Kod2 { get; set; }

    public short? Odemegunu { get; set; }

    public DateTime? Odemetarihi { get; set; }

    public string? KdvDahilmi { get; set; }

    public short? FatkalemAdedi { get; set; }

    public DateTime? SiparisTest { get; set; }

    public decimal? ToplamMik { get; set; }

    public short? Topdepo { get; set; }

    public string? Yedek22 { get; set; }

    public string? CariKod2 { get; set; }

    public string? Yedek { get; set; }

    public string? UpdateKodu { get; set; }

    public int Sirano { get; set; }

    public decimal? KdvDahilBrutTop { get; set; }

    public decimal? KdvTenzil { get; set; }

    public decimal? Malfazlasikdvsi { get; set; }

    public decimal? Geneltoplam { get; set; }

    public decimal? Yuvarlama { get; set; }

    public string? SatisKond { get; set; }

    public string? PlaKodu { get; set; }

    public byte? Doviztip { get; set; }

    public decimal? Doviztut { get; set; }

    public string? KsKodu { get; set; }

    public decimal? BagTutar { get; set; }

    public string? Yedek2 { get; set; }

    public string? HizmetFat { get; set; }

    public DateTime? Vadebazt { get; set; }

    public string? Kapatilmis { get; set; }

    public string? SYedek1 { get; set; }

    public string? SYedek2 { get; set; }

    public decimal? FYedek3 { get; set; }

    public decimal? FYedek4 { get; set; }

    public decimal? FYedek5 { get; set; }

    public string? CYedek6 { get; set; }

    public byte? BYedek7 { get; set; }

    public short? IYedek8 { get; set; }

    public int? LYedek9 { get; set; }

    public string? AmbarKblno { get; set; }

    public DateTime? DYedek10 { get; set; }

    public string? ProjeKodu { get; set; }

    public string? Kosulkodu { get; set; }

    public DateTime? Fiyattarihi { get; set; }

    public DateTime? Kosultarihi { get; set; }

    public short? Genisk1tip { get; set; }

    public short? Genisk2tip { get; set; }

    public short? Genisk3tip { get; set; }

    public byte? Exporttype { get; set; }

    public string? Exgumrukno { get; set; }

    public DateTime? Exgumtarih { get; set; }

    public DateTime? Exfiilitarih { get; set; }

    public string? Exportrefno { get; set; }

    public string? Kayityapankul { get; set; }

    public DateTime? Kayittarihi { get; set; }

    public string? Duzeltmeyapankul { get; set; }

    public DateTime? Duzeltmetarihi { get; set; }

    public short? GelsubeKodu { get; set; }

    public short? GitsubeKodu { get; set; }

    public string Onaytipi { get; set; } = null!;

    public int Onaynum { get; set; }

    public short IsletmeKodu { get; set; }

    public string? Odekod { get; set; }

    public decimal? Brmaliyet { get; set; }

    public short? Kosvadegunu { get; set; }

    public string? Yapkod { get; set; }

    public string? GibFatirsNo { get; set; }

    public string? Externalappid { get; set; }

    public string? Externalrefid { get; set; }

    public int? Ebelge { get; set; }

    public int? Halfat { get; set; }

    public decimal? FatAltm3 { get; set; }

    public DateTime? Dovbaztar { get; set; }

    public decimal? Otvtevtutar { get; set; }

    public short? Topgirdepo { get; set; }

    public string? Bform { get; set; }

    public int? Tevkifatiade { get; set; }

    public string? Naktevkodu { get; set; }

    public decimal? Naktevtutar { get; set; }

    public string? Faturalasmayacak { get; set; }
}
