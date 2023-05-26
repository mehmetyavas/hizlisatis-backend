using Core.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblcasabit:IEntity
{
    public short? SubeKodu { get; set; }

    public short? IsletmeKodu { get; set; }

    public string CariKod { get; set; } = null!;

    public string? CariTel { get; set; }

    public string? CariIl { get; set; }

    public string? UlkeKodu { get; set; }

    public string? CariIsim { get; set; }

    public string? CariTip { get; set; }

    public string? GrupKodu { get; set; }

    public string? RaporKodu1 { get; set; }

    public string? RaporKodu2 { get; set; }

    public string? RaporKodu3 { get; set; }

    public string? RaporKodu4 { get; set; }

    public string? RaporKodu5 { get; set; }

    public string? CariAdres { get; set; }

    public string? CariIlce { get; set; }

    public string? VergiDairesi { get; set; }

    public string? VergiNumarasi { get; set; }

    public string? Fax { get; set; }

    public string? Postakodu { get; set; }

    public short? DetayKodu { get; set; }

    public decimal? NakliyeKatsayisi { get; set; }

    public decimal? RiskSiniri { get; set; }

    public decimal? Teminati { get; set; }

    public decimal? Carisk { get; set; }

    public decimal? Ccrisk { get; set; }

    public decimal? Sarisk { get; set; }

    public decimal? Scrisk { get; set; }

    public decimal? CmBorct { get; set; }

    public decimal? CmAlact { get; set; }

    public DateTime? CmRapTarih { get; set; }

    public string? Kosulkodu { get; set; }

    public decimal? IskontoOrani { get; set; }

    public short? VadeGunu { get; set; }

    public byte? ListeFiati { get; set; }

    public string? Acik1 { get; set; }

    public string? Acik2 { get; set; }

    public string? Acik3 { get; set; }

    public string? MKod { get; set; }

    public byte? DovizTipi { get; set; }

    public byte? DovizTuru { get; set; }

    public string? Hesaptutmasekli { get; set; }

    public string? Dovizlimi { get; set; }

    public string? UpdateKodu { get; set; }

    public string? PlasiyerKodu { get; set; }

    public short? Lokaldepo { get; set; }

    public string? Email { get; set; }

    public string? Web { get; set; }

    public string? Kurfarkiborc { get; set; }

    public string? Kurfarkialac { get; set; }

    public string? SYedek1 { get; set; }

    public string? SYedek2 { get; set; }

    public decimal? FYedek1 { get; set; }

    public decimal? FYedek2 { get; set; }

    public string? CYedek1 { get; set; }

    public string? CYedek2 { get; set; }

    public byte? BYedek1 { get; set; }

    public short? IYedek1 { get; set; }

    public int? LYedek1 { get; set; }

    public string? Fiyatgrubu { get; set; }

    public string? Kayityapankul { get; set; }

    public DateTime? Kayittarihi { get; set; }

    public string? Duzeltmeyapankul { get; set; }

    public DateTime? Duzeltmetarihi { get; set; }

    public byte? Odemetipi { get; set; }

    public string? Onaytipi { get; set; }

    public int? Onaynum { get; set; }

    public string? Musteribazikdv { get; set; }

    public decimal? AgirlikIsk { get; set; }

    public string? CariTel2 { get; set; }

    public string? CariTel3 { get; set; }

    public string? Fax2 { get; set; }

    public string? Gsm1 { get; set; }

    public string? Gsm2 { get; set; }

    public string? Gekaphesaplanmasin { get; set; }

    public string? OncekiKod { get; set; }

    public string? SonrakiKod { get; set; }

    public string? Soncarikodu { get; set; }

    public string? Teslimcaribaglimi { get; set; }

    public string? Baglicarikod { get; set; }

    public string? FabrikaKodu { get; set; }

    public byte? NakliyeSuresi { get; set; }

    public string? TeslimatPeriyodTipi { get; set; }

    public byte? TeslimatGunu { get; set; }

    public string? TeslimatExtrainfo { get; set; }
}
